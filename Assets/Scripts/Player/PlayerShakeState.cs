using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerShakeState : PlayerState
    {
        private int _shakeForce = 0;
        private GameObject player;
        private Vector3 _previousPosition;
        private TextMeshProUGUI _shakeForceText;

        public void Initialize(GameObject player, TextMeshProUGUI shakeForceText)
        {
            this.player = player;
            this._shakeForceText = shakeForceText;
        }
        
        public override async Task Enter()
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            Plane plane = new Plane(Vector3.forward, player.transform.position);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 mouseWorldPosition = ray.GetPoint(distance);
                
                Vector3 offset = new Vector3(0, -5f, 0);

                MoveTowardsOffset(new Vector3(
                    mouseWorldPosition.x,
                    mouseWorldPosition.y + offset.y,
                    player.transform.position.z
                ));
            }
            await Task.Delay(1000);
        }

        public override void Update()
        {
            Vector3 mouseScreenPosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            Plane plane = new Plane(Vector3.forward, player.transform.position);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 mouseWorldPosition = ray.GetPoint(distance);
                
                Vector3 offset = new Vector3(0, -5f, 0);

                Vector3 newPosition = new Vector3(
                    mouseWorldPosition.x,
                    mouseWorldPosition.y + offset.y,
                    player.transform.position.z
                );
                float distanceMoved = Vector3.Distance(_previousPosition, newPosition);
                _shakeForce += Mathf.RoundToInt(distanceMoved);
                _shakeForceText.text = $"Shake Force: {_shakeForce}";
                _previousPosition = newPosition;
                player.transform.position = newPosition;
            }
        }
        
        private void MoveTowardsOffset(Vector3 newPosition)
        {
            player.transform.DOMove(newPosition, 0.1f);
        }

        public override void Exit()
        {
            Debug.Log($"Total Shake Force: {_shakeForce}");
        }
    }
}