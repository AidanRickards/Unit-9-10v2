
using UnityEngine;
namespace Player
{
    public class JumpState : State
    {
        // constructor
        public JumpState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            player.animator.Play("Jump", 0, 0);
            player.isJumping = true;
            base.Enter();
            Debug.Log("Jumping");
        }

        public override void Exit()
        {
            player.isJumping = false;
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
            player.CheckForIdle();
            player.CheckForDead();
            //player.Win();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Debug.Log("Jumping");
        }
    }
}