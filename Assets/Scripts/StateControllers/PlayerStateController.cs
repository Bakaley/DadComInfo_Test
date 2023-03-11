using System;
using MovementControllers;
using ScriptableObjects;
using States.MonsterStates;
using States.PlayerStates;
using UnityEngine;

namespace StateControllers
{
    [RequireComponent(typeof(PlayerMovementController))]
    public class PlayerStateController : BattleCharacterStateController
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private PlayerStatsSO _config;
        [SerializeField] private PlayerMovementController _playerMovementController;
        
        public PlayerStatsSO Config => _config;
        public CameraController CameraController => _cameraController;
        
        public void InitOnSpawn()
        {
            MaxHP = _config.HP;
            CurrentHP = MaxHP;
            SetDefaultState();
        }

        protected override void InitStateList()
        {
            States.Add(typeof(PlayerIdleState), new PlayerIdleState(this));
            States.Add(typeof(PlayerAttackingState), new PlayerAttackingState(this));
            States.Add(typeof(PlayerStaggeringState), new PlayerStaggeringState(this));
            States.Add(typeof(PlayerDeadState), new PlayerDeadState(this));
        }

        protected override void SetDefaultState()
        {
            SwitchState(States[typeof(PlayerIdleState)]);
        }

        protected override void Awake()
        {
            movementController = _playerMovementController;
            base.Awake();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DamageApplyBox box))
            {
                if (box.Owner.TryGetComponent(out MonsterStateController monster))
                {
                    SufferHitFrom(monster);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out HPSphere sphere))
            {
                if (CurrentHP < MaxHP)
                {
                    CurrentHP += sphere.HPGain;
                    Destroy(sphere.gameObject);
                }
            }
        }

        private void SufferHitFrom(MonsterStateController monster)
        {
            if (!Dead)
            {
                CurrentHP -= monster.Config.OnHitDamage;
                currentState.OnReceiveHit();
            }
        }
    }
}
