using UnityEngine;

public class FollowRagDoll : MonoBehaviour
{
    [SerializeField] private Transform ragdollRoot;
    [SerializeField] private Collider playerCollider;
    
    private void LateUpdate()
    {
        playerCollider.transform.position = ragdollRoot.position;
    }
}
