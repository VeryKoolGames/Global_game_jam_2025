using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class GameListener : MonoBehaviour
    {
        public GameEvent changeStateEvent;
        public UnityEvent Response;

        private void OnEnable() {
            changeStateEvent.RegisterListener(this);
        }

        private void OnDisable() {
            changeStateEvent.UnregisterListener(this);
        }

        public void OnEventRaised() {
            Response.Invoke();
        }
    }
}