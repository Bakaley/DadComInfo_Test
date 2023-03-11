using System.Collections;
using System.Collections.Generic;
using StateControllers;
using States;
using UnityEngine;

namespace States.MonsterStates
{
   public class MonsterDeadState : GeneralBattleCharacterState
   {
      public MonsterDeadState(BattleCharacterStateController stateController)
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
         stateController.Dead = true;
         ((MonsterStateController) stateController).HPBar.Hide();
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
         stateController.Dead = false;
      }
   }
}