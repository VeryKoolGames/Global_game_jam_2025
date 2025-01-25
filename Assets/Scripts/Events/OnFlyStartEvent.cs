using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "OnFlyStartEvent", menuName = "ScriptableObjects/Events/OnFlyStartEvent")]
    public class OnFlyStartEvent : UnityEngine.ScriptableObject
    {
        private List<OnFlyStartListener> listeners = new List<OnFlyStartListener>();

        public void Raise(float speed) {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(speed);
            }
        }

        public void RegisterListener(OnFlyStartListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnFlyStartListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}
