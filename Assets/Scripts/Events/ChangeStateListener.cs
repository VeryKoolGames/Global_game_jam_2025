using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class ChangeStateListener : MonoBehaviour
    {
        public ChangeStateEvent changeStateEvent;
        public UnityEvent<PlayerStateEnum> Response;

        private void OnEnable() {
            changeStateEvent.RegisterListener(this);
        }

        private void OnDisable() {
            changeStateEvent.UnregisterListener(this);
        }

        public void OnEventRaised(PlayerStateEnum state) {
            Response.Invoke(state);
        }
    }
}