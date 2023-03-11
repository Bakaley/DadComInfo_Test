namespace MovementControllers
{
    public interface IMovementController
    {
        public void SetMovementAllowed(bool newValue);
        public void SetAttackingMultiplier(bool newValue);
        public float CurrentSpeedMagnitude { get; }
    }
}
