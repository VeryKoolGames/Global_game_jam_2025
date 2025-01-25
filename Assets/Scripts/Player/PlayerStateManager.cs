using System;
using Events;
using KBCore.Refs;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerStateManager : MonoBehaviour
    {
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerShakeState ShakeState{ get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerFlyState FlyState { get; private set; }
        public PlayerReadyToFlyState ReadyToFlyState { get; private set; }
        public PlayerStateEnum currentStateEnum { get; private set; }
        [SerializeField, Self] ChangeStateListener changeStateListener;

        [Header("Shake State")] [SerializeField]
        private TextMeshProUGUI shakeText;
        [SerializeField] private RagdollManager ragdollManager;
        [SerializeField] private GameListener stopDragListener;
        
        [Header("Fly State")]
        [SerializeField] private GameListener refuelListener;
        [SerializeField] private OnFlyStartEvent onFlyStartEvent;
        
        [Header("Ready to Fly State")]
        [SerializeField] private Transform readyToFlyTransform;
        
        
        [SerializeField] private Animator playerAnimator;

        public void Start()
        {
            changeStateListener.Response.AddListener(ChangeState);
            StateMachine = new PlayerStateMachine();
            ShakeState = new PlayerShakeState();
            IdleState = new PlayerIdleState();
            FlyState = new PlayerFlyState();
            ReadyToFlyState = new PlayerReadyToFlyState();
            FlyState.Initialize(gameObject, playerAnimator, refuelListener, this, onFlyStartEvent);
            ShakeState.Initialize(gameObject, shakeText, ragdollManager, stopDragListener);
            ReadyToFlyState.Initialize(this, ragdollManager, playerAnimator, readyToFlyTransform.position, gameObject);
            IdleState.Initialize(playerAnimator);
            StateMachine.Initialize(IdleState);
        }

        public void FixedUpdate()
        {
            StateMachine.Update();
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