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
        Transform targetTransform;
        private GameObject player;
        private Animator canonAnimator;
        

        public void Initialize(PlayerStateManager playerStateManager, RagdollManager ragdollManager,
            Animator playerAnimator, Transform targetTransform, GameObject player, Animator canonAnimator)
        {
            this.canonAnimator = canonAnimator;
            this.playerStateManager = playerStateManager;
            this._ragdollManager = ragdollManager;
            this._playerAnimator = playerAnimator;
            this.targetTransform = targetTransform;
            this.player = player;
        }
        public override async Task Enter()
        {
            player.transform.rotation = Quaternion.identity;
            Debug.Log("Entered ReadyToFly state.");
            _ragdollManager.EnableRagdoll();
            _ragdollManager.RemoveYConstraint();
            canonAnimator.Play("Plateforme_OPEN");
            await Task.Delay(4000);
            _ragdollManager.DisableRagdoll();
            player.gameObject.transform.SetParent(targetTransform);
            Vector3 newPos = Vector3.zero;
            newPos.x = -0.2f;
            newPos.y = 2f;
            newPos.z = 1f;
            player.gameObject.transform.position = newPos;
            await Task.Delay(2000);
            SoundManager.instance.PlaySound(SoundType.MECHE);
            await Task.Delay(2700);
            SoundManager.instance.PlaySound(SoundType.MainMusic);
            player.transform.SetParent(null);
            playerStateManager.StateMachine.ChangeState(playerStateManager.FlyState);
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            SoundManager.instance.PlaySound(SoundType.Canon);
            Debug.Log("Exited ReadyToFly state.");
        }
    }
}