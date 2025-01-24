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
        [SerializeField, Self] ChangeStateListener changeStateListener;

        [Header("Shake State")] [SerializeField]
        private TextMeshProUGUI shakeText;

        public void Start()
        {
            changeStateListener.Response.AddListener(ChangeState);
            StateMachine = new PlayerStateMachine();
            ShakeState = new PlayerShakeState();
            IdleState = new PlayerIdleState();
            StateMachine.Initialize(IdleState);
            ShakeState.Initialize(this.gameObject, shakeText);
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
                    break;
                case PlayerStateEnum.IDLE:
                    StateMachine.ChangeState(IdleState);
                    break;
                case PlayerStateEnum.READY_TO_FLY:
                    StateMachine.ChangeState(IdleState);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}