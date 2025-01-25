using System.Threading.Tasks;
using DG.Tweening;
using Events;
using UnityEngine;

namespace Player
{
    public class PlayerReadyToFlyState : PlayerState
    {
        private PlayerStateManager playerStateManager;
        private RagdollManager _ragdollManager;
        private Animator _playerAnimator;
        Vector3 targetPosition;
        private GameObject player;
        

        public void Initialize(PlayerStateManager playerStateManager, RagdollManager ragdollManager, Animator playerAnimator, Vector3 targetPos, GameObject player)
        {
            this.playerStateManager = playerStateManager;
            this._ragdollManager = ragdollManager;
            this._playerAnimator = playerAnimator;
            this.targetPosition = targetPos;
            this.player = player;
        }
        public override async Task Enter()
        {
            Debug.Log("Entered ReadyToFly state.");
            _ragdollManager.EnableRagdoll();
            _ragdollManager.RemoveYConstraint();
            await Task.Delay(2000);
            _ragdollManager.DisableRagdoll();
            _playerAnimator.Play("ReadyToFlyAnim");
            player.transform.DOMove(targetPosition, 5f);
            await Task.Delay(5000);
            
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