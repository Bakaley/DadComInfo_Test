using MovementControllers;
using ScriptableObjects;
using States.MonsterStates;
using UI;
using UnityEngine;
using Zenject;

namespace StateControllers
{
    [RequireComponent(typeof(MonsterMovementController))]
    public class MonsterStateController : BattleCharacterStateController
    {
        [SerializeField] private MonsterStatsSO _config;
        [SerializeField] private MonsterMovementController _monsterMovementController;
        [SerializeField] private AttackRangeBox _attackRangeBox;
        [SerializeField] private MonsterHPBar _monsterHpBar;

        private readonly string _monsterLayerName = "Monster";
        private readonly string _deadMonsterLayerName = "DeadMonster";
        private int _monsterLayer;
        private int _deadMonsterLayer;
        
        private PlayerStateController _playerToAttack;
        private int _deathCounter = 0;
        public MonsterStatsSO Config => _config;
        public PlayerStateController PlayerToAttack => _playerToAttack;
        public AttackRangeBox AttackRangeBox => _attackRangeBox;
        public MonsterHPBar HPBar => _monsterHpBar;

        public void InitOnSpawn(PlayerStateController player, Vector3 newPos)
        {
            _deathCounter = 0;
            _playerToAttack = player;
            
            MaxHP = _config.HP;
            CurrentHP = MaxHP;
            _monsterMovementController.Warp(newPos);
            SwitchState(States[typeof(MonsterChasingState)]);
            HPBar.Show();

        }

        public void InitOnRespawn(Vector3 newPos)
        { 
            MaxHP = _config.HP + _deathCounter * _config.HPIncreasingPerDeath;
            CurrentHP = MaxHP;
            _monsterMovementController.Warp(newPos);
            SwitchState(States[typeof(MonsterChasingState)]);
            HPBar.Show();
        }
        
        protected override void InitStateList()
        {
            States.Add(typeof(MonsterIdleState), new MonsterIdleState(this));
            States.Add(typeof(MonsterChasingState), new MonsterChasingState(this));
            States.Add(typeof(MonsterAttackingState), new MonsterAttackingState(this));
            States.Add(typeof(MonsterStaggeringState), new MonsterStaggeringState(this));
            States.Add(typeof(MonsterDeadState), new MonsterDeadState(this));
        }
        
        protected override void SetDefaultState()
        {
            SwitchState(States[typeof(MonsterIdleState)]);
        }

        protected override void Awake()
        {
            _monsterLayer = LayerMask.NameToLayer(_monsterLayerName);
            _deadMonsterLayer = LayerMask.NameToLayer(_deadMonsterLayerName);
            
            movementController = _monsterMovementController;
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            _monsterMovementController.Init(this);
            _monsterHpBar.Init(this);
        }

        protected override void SetDeadHandler(bool newValue)
        {
            if (newValue)
            {
                _deathCounter++;
                gameObject.layer = _deadMonsterLayer;
            }
            else gameObject.layer = _monsterLayer;
            
            base.SetDeadHandler(newValue);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DamageApplyBox box))
            {
                if (box.Owner.TryGetComponent(out PlayerStateController player))
                {
                    SufferHitFrom(player);
                }
            }
        }

        private void SufferHitFrom(PlayerStateController player)
        {
            if (!Dead)
            {
                CurrentHP -= player.Config.OnHitDamage;
                currentState.OnReceiveHit();
            }
        }
    }
}