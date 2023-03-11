using StateControllers;

namespace States.MonsterStates
{
    public class MonsterIdleState : GeneralBattleCharacterState
    {
        public MonsterIdleState(BattleCharacterStateController stateController) : base(stateController)
        {
        }

        public override void OnReceiveHit()
        {
            
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            stateController.AnimatorController.SetMovementValue(stateController.MovementController.CurrentSpeedMagnitude);
        }

        public override void FixedUpdate()
        {
            
        }
    }
}
