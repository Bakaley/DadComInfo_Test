using StateControllers;

namespace States
{
    public abstract class GeneralBattleCharacterState : IState
    {
        protected BattleCharacterStateController stateController;
        public GeneralBattleCharacterState(BattleCharacterStateController stateController)
        {
            this.stateController = stateController;
        }
        public abstract void OnReceiveHit();
        public abstract void Enter();
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void Exit();
    }
}