using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/Events/GameEvent")]
    public class GameEvent : UnityEngine.ScriptableObject
    {
        private List<GameListener> listeners = new List<GameListener>();

        public void Raise() {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(GameListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}
