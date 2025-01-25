using System.Threading.Tasks;
using UnityEngine;

namespace Player
{
    public class PlayerIdleState : PlayerState
    {
        private Animator playerAnimator;
        public void Initialize(Animator playerAnimator)
        {
            Debug.Log("PlayerIdleState initialized.");
            this.playerAnimator = playerAnimator;
        }
        
        public override Task Enter()
        {
            playerAnimator.Play("Idle");
            return Task.CompletedTask;
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
            Debug.Log("PlayerIdleState exited.");
            // playerAnimator.CrossFade("Empty", 0.1f);
        }
    }
}