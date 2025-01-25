using System.Threading.Tasks;
using Events;
using UnityEngine;

namespace Player
{
    public class PlayerDeathState : PlayerState
    {
        private RagdollManager _ragdollManager;
        private GameEvent playerDeathEvent;
        public void Initialize(RagdollManager _ragdollManager, GameEvent playerDeathEvent)
        {
            this._ragdollManager = _ragdollManager;
            this.playerDeathEvent = playerDeathEvent;
        }
        
        public override Task Enter()
        {
            playerDeathEvent.Raise();
            _ragdollManager.EnableRagdoll();
            return Task.CompletedTask;
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
        }
    }
}