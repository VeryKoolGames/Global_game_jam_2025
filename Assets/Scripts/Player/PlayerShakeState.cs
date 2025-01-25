using System.Threading.Tasks;
using DG.Tweening;
using Events;
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
        private RagdollManager _ragdollManager;
        private GameListener stopDragListener;
        private bool shouldDrag = false;


        public void Initialize(GameObject player, TextMeshProUGUI shakeForceText, RagdollManager ragdollManager, GameListener stopDragListener)
        {
            Debug.Log("PlayerShakeState initialized.");
            this.player = player;
            this._shakeForceText = shakeForceText;
            this._ragdollManager = ragdollManager;
            this.stopDragListener = stopDragListener;
            this.stopDragListener.Response.AddListener(onDragStopped);
        }
        
        public override async Task Enter()
        {
            _ragdollManager.EnableRagdoll();
            Vector3 mouseScreenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            Plane plane = new Plane(Vector3.forward, player.transform.position);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 mouseWorldPosition = ray.GetPoint(distance);
                
                Vector3 offset = new Vector3(0, -3f, 0);

                MoveTowardsOffset(new Vector3(
                    mouseWorldPosition.x,
                    mouseWorldPosition.y + offset.y,
                    player.transform.position.z
                ));
            }
            await Task.Delay(1000);
        }
        
        private void onDragStopped()
        {
            shouldDrag = !shouldDrag;
            if (!shouldDrag)
                _ragdollManager.RemoveYConstraint();
            else
                _ragdollManager.EnableYConstraint();
        }

        public override void Update()
        {
            if (!shouldDrag)
                return;
            Vector3 mouseScreenPosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            Plane plane = new Plane(Vector3.forward, player.transform.position);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 mouseWorldPosition = ray.GetPoint(distance);
                
                Vector3 offset = new Vector3(0, -3f, 0);

                Vector3 newPosition = new Vector3(
                    mouseWorldPosition.x,
                    mouseWorldPosition.y + offset.y,
                    player.transform.position.z
                );
                float distanceMoved = Vector3.Distance(_previousPosition, newPosition);
                if (distanceMoved > 0.5f)
                {
                    _ragdollManager.ShakeRagdoll(newPosition, _previousPosition);
                }
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
            stopDragListener.Response.RemoveListener(onDragStopped);
            _ragdollManager.DisableRagdoll();
            Debug.Log("Exited Shake state.");
            Debug.Log($"Total Shake Force: {_shakeForce}");
        }
    }
}