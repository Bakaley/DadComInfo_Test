using System.Collections;
using StateControllers;
using UnityEngine;

namespace States.MonsterStates
{
    public class MonsterAttackingState : GeneralBattleCharacterState
    {
        private Coroutine _attackingCoroutine;

        public MonsterAttackingState(BattleCharacterStateController stateController)
            : base(stateController)
        {

        }

        public override void Enter()
        {
            float attackingDuration = ((MonsterStateController)stateController).Config.AttackDuration;
            stateController.AnimatorController.Attack(attackingDuration);
            stateController.AnimatorController.OnAttackApplyEffect += AttackApplyHandler;
            stateController.MovementController.SetAttackingMultiplier(true);
            _attackingCoroutine = stateController.StartCoroutine(EndAttackCoroutine(attackingDuration));
        }

        public override void OnReceiveHit()
        {
            if(_attackingCoroutine != null) stateController.StopCoroutine(_attackingCoroutine);
            stateController.AnimatorController.OnAttackApplyEffect -= AttackApplyHandler;
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
            stateController.MovementController.SetAttackingMultiplier(false);
        }
        
        private void AttackApplyHandler()
        {
            stateController.CastAttackBox();
        }
        
        private IEnumerator EndAttackCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            
            stateController.AnimatorController.OnAttackApplyEffect -= AttackApplyHandler;
            stateController.SwitchState(stateController.States[typeof(MonsterChasingState)]);
            _attackingCoroutine = null;
        }
    }
}