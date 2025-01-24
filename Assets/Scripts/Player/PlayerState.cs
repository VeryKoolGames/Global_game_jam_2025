using UnityEngine;

namespace Player
{
    public abstract class PlayerState
    {
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
        public GameObject Player;
    }
}