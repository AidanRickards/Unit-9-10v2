
using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
namespace Player
{
    public class RunningState : State
    {
        private float _horizontalInput;

        public Rigidbody2D rb;
        public JoystickMove jm;
        public float speed = 5;

        

        // constructor
        public RunningState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            player.animator.Play("Run", 0, 0);
            base.Enter();
            Debug.Log("Running");
            player.loop.clip = player.walk;
            player.loop.Play();
        }

        public override void Exit()
        {
            player.loop.Stop();
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            player.CheckForIdle();
            player.CheckForJump();
            player.CheckForDead();
            //player.Win();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            //running code

            int x = player.jm.dirX;
            int y = player.jm.dirY;

            //player.rb.velocity = new Vector2(move.x * speed, player.rb.velocity.y);
            player.rb.linearVelocity = new Vector2(x * player.jm.playerSpeed, y);
            Debug.Log("Running");

            if (x == 1)
            {
                player.sr.flipX = true;
            }
            if (x == -1)
            {
                player.sr.flipX = false;
            }
        }
    }
}