using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class OnFlyStartListener : MonoBehaviour
    {
        public OnFlyStartEvent OnFlyStartEvent;
        public UnityEvent<float> Response;

        private void OnEnable() {
            OnFlyStartEvent.RegisterListener(this);
        }

        private void OnDisable() {
            OnFlyStartEvent.UnregisterListener(this);
        }

        public void OnEventRaised(float speed) {
            Response.Invoke(speed);
        }
    }
}