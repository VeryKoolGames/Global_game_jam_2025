using System.Threading.Tasks;
using Events;
using UnityEngine;

namespace Player
{
    public class PlayerReadyToFlyState : PlayerState
    {
        private PlayerStateManager playerStateManager;
        private GameListener refuelListener;
        public float TotalFuel { get; private set; }

        public void Initialize(PlayerStateManager playerStateManager, GameListener refuelListener)
        {
            this.playerStateManager = playerStateManager;
            this.refuelListener = refuelListener;
            this.refuelListener.Response.AddListener(refillFuel);
        }
        public override async Task Enter()
        {
            Debug.Log("Entered ReadyToFly state.");
            await Task.Delay(100);
            playerStateManager.StateMachine.ChangeState(playerStateManager.FlyState);
        }
        
        private void refillFuel()
        {
            TotalFuel += 10;
        }

        public override void Update()
        {
            TotalFuel -= Time.deltaTime;
            if (TotalFuel <= 0)
            {
                playerStateManager.StateMachine.ChangeState(playerStateManager.IdleState);
            }
        }

        public override void Exit()
        {
            this.refuelListener.Response.RemoveListener(refillFuel);
            Debug.Log("Exited ReadyToFly state.");
        }
    }
}