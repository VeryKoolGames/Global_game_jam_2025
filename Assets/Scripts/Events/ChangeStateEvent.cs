using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "OnBoostEvent", menuName = "ScriptableObjects/Events/OnBoostEvent")]
    public class ChangeStateEvent : UnityEngine.ScriptableObject
    {
        private List<ChangeStateListener> listeners = new List<ChangeStateListener>();

        public void Raise(PlayerStateEnum state) {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(state);
            }
        }

        public void RegisterListener(ChangeStateListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(ChangeStateListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}
