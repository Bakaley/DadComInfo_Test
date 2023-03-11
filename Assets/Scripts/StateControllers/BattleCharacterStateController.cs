using System;
using System.Collections;
using System.Collections.Generic;
using MovementControllers;
using States;
using UnityEngine;
using UnityEngine.Serialization;

namespace StateControllers
{
    public abstract class BattleCharacterStateController : MonoBehaviour
    {
        private int _currentHP;
        private bool _dead = false;
        private Dictionary<Type, GeneralBattleCharacterState> _states = new Dictionary<Type, GeneralBattleCharacterState>();

        //IMovementController must be assigned in each inheritor
        protected IMovementController movementController;
        protected GeneralBattleCharacterState currentState;
        [SerializeField] protected BattleCharacterAnimatorController animatorController;
        [FormerlySerializedAs("boxCastOnAttack")] [SerializeField] protected DamageApplyBox boxCastOnDamageApply;
        
        public int CurrentHP
        {
            get => _currentHP;
            protected set
            {
                _currentHP = value;
                OnHpChanged?.Invoke(value);
            }
        }

        public bool Dead
        {
            get => _dead;
            set => SetDeadHandler(value);
        }
        
        public event Action<BattleCharacterStateController> OnDie;
        public event Action<int> OnHpChanged;

        public Dictionary<Type, GeneralBattleCharacterState> States => _states;
        public IMovementController MovementController => movementController;
        public BattleCharacterAnimatorController AnimatorController => animatorController;
        public int MaxHP { get; protected set; }

        public void SwitchState(GeneralBattleCharacterState state)
        {
            currentState?.Exit();
            currentState = state;
            currentState.Enter();
        }

        public void CastAttackBox()
        {
            StartCoroutine(AttackBoxCastCoroutine());
        }

        protected virtual void SetDeadHandler(bool newValue)
        {
            _dead = newValue;
            if (newValue) OnDie?.Invoke(this);
        }

        protected virtual void Awake()
        {
            InitStateList();
            boxCastOnDamageApply.Init(this);
        }

        protected virtual void Start()
        {
            SetDefaultState();
        }

        protected virtual void Update()
        {
            currentState.Update();
        }

        protected virtual void FixedUpdate()
        {
            currentState.FixedUpdate();
        }
    
        protected virtual void InitStateList()
        {
        
        }

        protected virtual void SetDefaultState()
        {
        
        }
        
        private IEnumerator AttackBoxCastCoroutine()
        {
            boxCastOnDamageApply.gameObject.SetActive(true);
            yield return new WaitForFixedUpdate();
            boxCastOnDamageApply.gameObject.SetActive(false);
        }
    }
}
