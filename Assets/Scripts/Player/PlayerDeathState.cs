using System.Threading.Tasks;
using Events;
using UnityEngine;

namespace Player
{
    public class PlayerDeathState : PlayerState
    {
        private RagdollManager _ragdollManager;
        public void Initialize(RagdollManager _ragdollManager)
        {
            this._ragdollManager = _ragdollManager;
        }
        
        public override Task Enter()
        {
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