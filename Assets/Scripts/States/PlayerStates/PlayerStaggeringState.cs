using System.Collections;
using StateControllers;
using UnityEngine;

namespace States.PlayerStates
{
   public class PlayerStaggeringState : GeneralBattleCharacterState
   {
      private Coroutine _staggeringCoroutine;
      
      public PlayerStaggeringState(BattleCharacterStateController stateController)
         : base(stateController)
      {
       
      }

      public override void OnReceiveHit()
      {
         if(_staggeringCoroutine != null) stateController.StopCoroutine(_staggeringCoroutine);
         
         if(stateController.CurrentHP <= 0) stateController.SwitchState(stateController.States[typeof(PlayerDeadState)]);
         else stateController.SwitchState(stateController.States[typeof(PlayerStaggeringState)]);
      }

      public override void Enter()
      {
         float staggeringDuration = ((PlayerStateController)stateController).Config.StaggeringDuration;
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
         stateController.SwitchState(stateController.States[typeof(PlayerIdleState)]);
         _staggeringCoroutine = null;
      }
   }
}
