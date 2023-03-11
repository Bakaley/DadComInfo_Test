using StateControllers;

namespace States.PlayerStates
{
   public class PlayerDeadState : GeneralBattleCharacterState
   {
      public PlayerDeadState(BattleCharacterStateController stateController)
         : base(stateController)
      {
       
      }

      public override void OnReceiveHit()
      {
         
      }

      public override void Enter()
      {
         stateController.AnimatorController.SetDead(true);
         stateController.MovementController.SetMovementAllowed(false);
         ((PlayerStateController) stateController).CameraController.enabled = false;
         stateController.Dead = true;
      }

      public override void Update()
      {
         
      }

      public override void FixedUpdate()
      {
         
      }

      public override void Exit()
      {
         stateController.AnimatorController.SetDead(false);
         stateController.MovementController.SetMovementAllowed(true);
         ((PlayerStateController) stateController).CameraController.enabled = true;
         stateController.Dead = false;
      }
   }
}
