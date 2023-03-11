using States;

namespace StateControllers
{
    public interface IStateController
    {
        public void SwitchState(IState state);
    }
}
