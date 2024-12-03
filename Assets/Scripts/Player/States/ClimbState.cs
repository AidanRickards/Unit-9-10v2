
using UnityEngine;
namespace Player
{
    public class ClimbState : State
    {
        // constructor
        public ClimbState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            player.animator.Play("Climb", 0, 0);
            base.Enter();
            Debug.Log("Climbing");
            player.bx.isTrigger = true;
            player.rb.gravityScale = 0;

            player.loop.clip = player.ladderClimb;
            player.loop.Play();

        }

        public override void Exit()
        {
            player.bx.isTrigger = false;
            player.rb.gravityScale = 1;
            player.loop.Stop();
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            player.CheckForDead();
            //player.Win();

            base.LogicUpdate();

            if(player.jm.dirY == 1 && player.IsNearLadderTop())
            {
                player.CheckForIdle();
            }

            if (player.jm.dirY == -1 && player.IsNearLadderBottom())
            {
                player.CheckForIdle();
            }
        }

        public override void PhysicsUpdate()
        {
            player.rb.linearVelocity = new Vector2(0, player.jm.dirY * 0.7f);

            base.PhysicsUpdate();
            Debug.Log("Climbing");
        }
    }
}
