using UnityEngine;

namespace Player
{
    public class PlayerShakeState : PlayerState
    {
        private int _shakeForce = 0;
        private GameObject player;
        private Vector3 _previousPosition;

        public void Initialize(GameObject player)
        {
            this.player = player;
        }
        
        public override void Enter()
        {
        }

        public override void Update()
        {
            Vector3 mouseScreenPosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            Plane plane = new Plane(Vector3.forward, player.transform.position);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 mouseWorldPosition = ray.GetPoint(distance);

                Vector3 newPosition = new Vector3(
                    mouseWorldPosition.x,
                    mouseWorldPosition.y,
                    player.transform.position.z
                );
                float distanceMoved = Vector3.Distance(_previousPosition, newPosition);
                _shakeForce += Mathf.RoundToInt(distanceMoved);
                _previousPosition = newPosition;
                player.transform.position = newPosition;
            }
        }

        public override void Exit()
        {
            Debug.Log($"Total Shake Force: {_shakeForce}");
        }
    }
}