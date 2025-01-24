using System.Threading.Tasks;

namespace Player
{
    public class PlayeReadyToFlyState : PlayerState
    {
        public override Task Enter()
        {
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