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
        private GameEvent onPlayerDeathEvent;
        private Material bottleMaterial;


        public void Initialize(GameObject player, Animator playerAnimator,
            GameListener refuelListener, PlayerStateManager _playerStateManager,
            OnFlyStartEvent onFlyStartEvent, GameEvent onPlayerDeathEvent, Material bottleMaterial)
        {
            this.bottleMaterial = bottleMaterial;
            this.onPlayerDeathEvent = onPlayerDeathEvent;
            this.player = player;
            this.playerAnimator = playerAnimator;
            this.refuelListener = refuelListener;
            this.refuelListener.Response.AddListener(refillFuel);
            this._playerStateManager = _playerStateManager;
            this.onFlyStartEvent = onFlyStartEvent;
        }

        private void UpdateMaterialOffset()
        {
            float normalizedFuel = Mathf.Clamp01(TotalFuel / 100f);
            bottleMaterial.SetFloat("_offset", 1 - normalizedFuel);
        }
        
        public override Task Enter()
        {
            TotalFuel = 100;
            Debug.Log("Entered Fly state.");
            SetPlayerHeight();
            targetPosition = player.transform.position;
            playerAnimator.Play("Flying");
            onFlyStartEvent.Raise(TotalFuel);
            return Task.CompletedTask;
        }

        private void SetPlayerHeight()
        {
            player.transform.position = new Vector3(player.transform.position.x, 2, player.transform.position.z);
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
            UpdateMaterialOffset();
            if (TotalFuel <= 0)
            {
                onPlayerDeathEvent.Raise();
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