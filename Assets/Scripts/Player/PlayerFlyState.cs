using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class PlayerFlyState : PlayerState
    {
        private GameObject player;
        Vector3 targetPosition;
        private float moveSpeed = 20f;
        private Tween moveTween;

        public void Initialize(GameObject player)
        {
            this.player = player;
        }
        public override Task Enter()
        {
            Debug.Log("Entered Fly state.");
            targetPosition = player.transform.position;
            return Task.CompletedTask;
        }

        public override void Update()
        {
            HandleInput();
            MovePlayer();
        }

        private void HandleInput()
        {
            if (Input.GetKey(KeyCode.A))
            {
                targetPosition += Vector3.left * moveSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                targetPosition += Vector3.right * moveSpeed * Time.deltaTime;
            }
        }

        private void MovePlayer()
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        public override void Exit()
        {
            Debug.Log("Exited Fly state."); 
        }
    }
}