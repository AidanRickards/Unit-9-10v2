using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class WinState : State
    {
        // constructor
        public WinState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            SceneManager.LoadScene(1);
            player.noLoopSource.PlayOneShot(player.win);
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