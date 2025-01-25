using System;
using Events;
using UnityEngine;

namespace Player
{
    public class ShakeManager : MonoBehaviour
    {
        [SerializeField] private PlayerStateManager _stateManager;
        private int _shakeForce;
        [SerializeField] private GameEvent startTimerEvent;
        [SerializeField] private GameEvent stopDragEvent;

        private void OnMouseDown()
        {
            if (_stateManager.currentStateEnum >= PlayerStateEnum.READY_TO_FLY)
                return;
            if (_stateManager.StateMachine._currentState != _stateManager.ShakeState)
            {
                _stateManager.StateMachine.ChangeState(_stateManager.ShakeState);
                startTimerEvent.Raise();
            }
            stopDragEvent.Raise();
        }

        private void OnMouseUp()
        {
            if (_stateManager.currentStateEnum >= PlayerStateEnum.READY_TO_FLY)
                return;
            stopDragEvent.Raise();
        }
    }
}
