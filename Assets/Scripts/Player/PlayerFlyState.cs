using System.Threading.Tasks;
using DG.Tweening;
using Events;
using UnityEngine;

namespace Player
{
    public class PlayerFlyState : PlayerState
    {
        private GameObject player;
        Vector3 targetPosition;
        private float moveSpeed = 20f;
        private Tween moveTween;
        private Animator playerAnimator;
        private float rotationAmount = 50f;
        private float rotationSpeed = 5f;
        public float TotalFuel { get; private set; }
        private GameListener refuelListener;
        private PlayerStateManager _playerStateManager;
        private OnFlyStartEvent onFlyStartEvent;


        public void Initialize(GameObject player, Animator playerAnimator, GameListener refuelListener, PlayerStateManager _playerStateManager, OnFlyStartEvent onFlyStartEvent)
        {
            this.player = player;
            this.playerAnimator = playerAnimator;
            this.refuelListener = refuelListener;
            this.refuelListener.Response.AddListener(refillFuel);
            this._playerStateManager = _playerStateManager;
            this.onFlyStartEvent = onFlyStartEvent;
        }
        public override Task Enter()
        {
            TotalFuel = 100;
            Debug.Log("Entered Fly state.");
            targetPosition = player.transform.position;
            playerAnimator.CrossFade("Flying", 0.8f);
            onFlyStartEvent.Raise(TotalFuel);
            return Task.CompletedTask;
        }
        
        private void refillFuel()
        {
            TotalFuel += 10;
        }

        public override void Update()
        {
            HandleInput();
            MovePlayer();
            TotalFuel -= Time.deltaTime;
            if (TotalFuel <= 0)
            {
                _playerStateManager.StateMachine.ChangeState(_playerStateManager.IdleState);
            }
        }

        private void HandleInput()
        {
            float targetRotationZ = 0f;

            if (Input.GetKey(KeyCode.A))
            {
                targetPosition += Vector3.left * moveSpeed * Time.deltaTime;
                targetRotationZ = rotationAmount;

            }
            else if (Input.GetKey(KeyCode.D))
            {
                targetPosition += Vector3.right * moveSpeed * Time.deltaTime;
                targetRotationZ = -rotationAmount;

            }
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetRotationZ);
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        private void MovePlayer()
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        public override void Exit()
        {
            Debug.Log("Exited Fly state."); 
        }
    }
}