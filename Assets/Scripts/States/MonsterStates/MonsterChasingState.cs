using StateControllers;
using UnityEngine;

namespace States.MonsterStates
{
    public class MonsterChasingState : GeneralBattleCharacterState
    {
        private AttackRangeBox _attackRangeBox;
        public MonsterChasingState(BattleCharacterStateController stateController)
            : base(stateController)
        {
            _attackRangeBox = ((MonsterStateController) stateController).AttackRangeBox;
        }

        public override void Enter()
        {
            stateController.MovementController.SetMovementAllowed(true);
            _attackRangeBox.OnTriggerStayEvent += AttackBoxStayHandler;
        }

        public override void OnReceiveHit()
        {
            if(stateController.CurrentHP <= 0) stateController.SwitchState(stateController.States[typeof(MonsterDeadState)]);
            else stateController.SwitchState(stateController.States[typeof(MonsterStaggeringState)]);
        }

        public override void Update()
        {
            stateController.AnimatorController.SetMovementValue(stateController.MovementController.CurrentSpeedMagnitude);
        }

        public override void FixedUpdate()
        {
            
        }

        public override void Exit()
        {
            _attackRangeBox.OnTriggerStayEvent -= AttackBoxStayHandler;
        }

        private void AttackBoxStayHandler(Collider other)
        {
            if (other.TryGetComponent(out PlayerStateController player))
            {
                stateController.SwitchState(stateController.States[typeof(MonsterAttackingState)]);
            }
        }
    }
}