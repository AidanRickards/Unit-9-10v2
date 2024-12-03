using UnityEngine;
namespace Player
{
    public class DeadState : State
    {
        // constructor
        public DeadState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            player.animator.Play("Death", 0, 0);
            player.noLoopSource.PlayOneShot(player.death);
            player.bx.isTrigger = true;
            base.Enter();
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

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
