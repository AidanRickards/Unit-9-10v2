
using UnityEngine;
namespace Player
{
    public class IdleState : State
    {
        // constructor
        public IdleState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            player.animator.Play("Idle", 0, 0);
            base.Enter();
            Debug.Log("Idle");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            player.CheckForRun();
            player.CheckForJump();
            player.CheckForLadderClimb();
            player.CheckForDead();
            //player.Win();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Debug.Log("Idle");
        }
    }
}