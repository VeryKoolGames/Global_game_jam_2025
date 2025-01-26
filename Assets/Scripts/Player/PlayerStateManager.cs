using System;
using Events;
using KBCore.Refs;
using ScriptableObject;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace Player
{
    public class PlayerStateManager : MonoBehaviour
    {
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerShakeState ShakeState{ get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerFlyState FlyState { get; private set; }
        public PlayerDeathState DeathState { get; private set; }
        public PlayerReadyToFlyState ReadyToFlyState { get; private set; }
        public PlayerStateEnum currentStateEnum { get; private set; }
        [SerializeField, Self] ChangeStateListener changeStateListener;

        [Header("Shake State")] [SerializeField]
        private TextMeshProUGUI shakeText;
        [SerializeField] private RagdollManager ragdollManager;
        [SerializeField] private GameListener stopDragListener;
        [SerializeField] private Rigidbody targetRigidbody;
        [SerializeField] private Volume postProcessingVolume;
        [SerializeField] private ParticleSystem bubbleParticleSystem;
        
        [Header("Fly State")]
        [SerializeField] private GameListener refuelListener;
        [SerializeField] private OnFlyStartEvent onFlyStartEvent;
        [SerializeField] private Material bottleMaterial;
        [SerializeField] private GameObject GoObject;
        [SerializeField] private Animator canvasAnimator;
        
        [Header("Ready to Fly State")]
        [SerializeField] private Transform readyToFlyTransform;
        [SerializeField] private Animator canonAnimimator;
        
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private GameListener playerDeathListener;
        [SerializeField] private GameEvent onPlayerDeathEvent;

        public void Start()
        {
            bottleMaterial.SetFloat("_offset", 0);
            playerDeathListener.Response.AddListener(OnPlayerDeath);
            changeStateListener.Response.AddListener(ChangeState);
            StateMachine = new PlayerStateMachine();
            ShakeState = new PlayerShakeState();
            IdleState = new PlayerIdleState();
            FlyState = new PlayerFlyState();
            ReadyToFlyState = new PlayerReadyToFlyState();
            DeathState = new PlayerDeathState();
            DeathState.Initialize(ragdollManager);
            FlyState.Initialize(gameObject, playerAnimator, refuelListener, this,
                onFlyStartEvent, onPlayerDeathEvent, bottleMaterial, GoObject, canvasAnimator);
            ShakeState.Initialize(gameObject, shakeText, ragdollManager, stopDragListener, bubbleParticleSystem, postProcessingVolume);
            ReadyToFlyState.Initialize(this, ragdollManager, playerAnimator, readyToFlyTransform, gameObject, canonAnimimator);
            IdleState.Initialize(playerAnimator);
            StateMachine.Initialize(IdleState);
        }
        
        private void OnPlayerDeath()
        {
            StateMachine.ChangeState(DeathState);
        }

        public void FixedUpdate()
        {
            StateMachine.Update();
        }

        private void OnDisable()
        {
            playerDeathListener.Response.RemoveListener(OnPlayerDeath);
            changeStateListener.Response.RemoveListener(ChangeState);
        }

        private void ChangeState(PlayerStateEnum state)
        {
            switch (state)
            {
                case PlayerStateEnum.SHAKE:
                    StateMachine.ChangeState(ShakeState);
                    currentStateEnum = PlayerStateEnum.SHAKE;
                    break;
                case PlayerStateEnum.IDLE:
                    StateMachine.ChangeState(IdleState);
                    currentStateEnum = PlayerStateEnum.IDLE;
                    break;
                case PlayerStateEnum.READY_TO_FLY:
                    StateMachine.ChangeState(ReadyToFlyState);
                    currentStateEnum = PlayerStateEnum.READY_TO_FLY;
                    break;
                case PlayerStateEnum.FLY:
                    StateMachine.ChangeState(FlyState);
                    currentStateEnum = PlayerStateEnum.FLY;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}