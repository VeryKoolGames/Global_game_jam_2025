using UnityEngine;

public class FollowRagDoll : MonoBehaviour
{
    [SerializeField] private Transform ragdollRoot;
    
    private void LateUpdate()
    {
        Vector3 ragdollRootPosition = ragdollRoot.position;
        ragdollRootPosition.z = 0;
        transform.position = ragdollRootPosition;
    }
}
