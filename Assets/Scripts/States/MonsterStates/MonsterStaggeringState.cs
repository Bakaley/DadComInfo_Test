using System.Collections;
using StateControllers;
using UnityEngine;

namespace States.MonsterStates
{
   public class MonsterStaggeringState : GeneralBattleCharacterState
   {
      private Coroutine _staggeringCoroutine;
      public MonsterStaggeringState(BattleCharacterStateController stateController)
         : base(stateController)
      {

      }

      public override void OnReceiveHit()
      {
         if(_staggeringCoroutine != null) stateController.StopCoroutine(_staggeringCoroutine);

         if(stateController.CurrentHP <= 0) stateController.SwitchState(stateController.States[typeof(MonsterDeadState)]);
         else stateController.SwitchState(stateController.States[typeof(MonsterStaggeringState)]);
      }

      public override void Enter()
      {
         float staggeringDuration = ((MonsterStateController)stateController).Config.StaggeringDuration;
         stateController.AnimatorController.GetHitted(staggeringDuration);
         _staggeringCoroutine = stateController.StartCoroutine(StaggeringCoroutine(staggeringDuration));
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
         
      }

      private IEnumerator StaggeringCoroutine(float duration)
      {
         yield return new WaitForSeconds(duration);
         stateController.SwitchState(stateController.States[typeof(MonsterChasingState)]);
         _staggeringCoroutine = null;
      }
   }
}