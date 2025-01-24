using System.Threading.Tasks;
using UnityEngine;

namespace Player
{
    public class PlayerReadyToFlyState : PlayerState
    {
        private PlayerStateManager playerStateManager;

        public void Initialize(PlayerStateManager playerStateManager)
        {
            this.playerStateManager = playerStateManager;
        }
        public override async Task Enter()
        {
            Debug.Log("Entered ReadyToFly state.");
            await Task.Delay(100);
            playerStateManager.StateMachine.ChangeState(playerStateManager.FlyState);
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
            Debug.Log("Exited ReadyToFly state.");
        }
    }
}