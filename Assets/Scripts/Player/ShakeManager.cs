using System;
using Events;
using KBCore.Refs;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerStateManager))]
    public class ShakeManager : MonoBehaviour
    {
        [SerializeField, Self] private PlayerStateManager _stateManager;
        private int _shakeForce;
        [SerializeField] private GameEvent startTimerEvent;

        private void OnMouseDown()
        {
            if (_stateManager.StateMachine._currentState == _stateManager.ShakeState)
                return;
            startTimerEvent.Raise();
            _stateManager.StateMachine.ChangeState(_stateManager.ShakeState);
        }

        private void OnMouseUp()
        {
        }
    }
}
