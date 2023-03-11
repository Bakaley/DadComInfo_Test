using System;
using StateControllers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace States.PlayerStates
{
    public class PlayerIdleState : GeneralBattleCharacterState
    {
        private PlayerInput playerInput;
        
        public PlayerIdleState(BattleCharacterStateController stateController)
            : base(stateController)
        {
            playerInput = new PlayerInput();
        }
    
        public override void OnReceiveHit()
        {
            if(stateController.CurrentHP <= 0) stateController.SwitchState(stateController.States[typeof(PlayerDeadState)]);
            else stateController.SwitchState(stateController.States[typeof(PlayerStaggeringState)]);
        }

        public override void Enter()
        {
            stateController.MovementController.SetMovementAllowed(true);

            playerInput.Enable();
            playerInput.KeyboardMouse.Attack.performed += Attack();
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
            playerInput.KeyboardMouse.Attack.performed -= Attack();
            playerInput.Disable();
        }

        private Action<InputAction.CallbackContext> Attack()
        
        {
            return _ =>
            {
                stateController.SwitchState(stateController.States[typeof(PlayerAttackingState)]);
            };
        }
    }
}
