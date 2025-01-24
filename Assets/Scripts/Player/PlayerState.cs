using System.Threading.Tasks;
using UnityEngine;

namespace Player
{
    public abstract class PlayerState
    {
        public abstract Task Enter();
        public abstract void Update();
        public abstract void Exit();
        public GameObject Player;
    }
}