using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    private Rigidbody[] ragdollBodies;
    private Collider[] ragdollColliders;
    private Animator animator;
    public Rigidbody _mainRb;

    private void Start()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        animator = GetComponent<Animator>();

        DisableRagdoll();
    }

    public void EnableRagdoll()
    {
        if (animator) animator.enabled = false;

        foreach (var rb in ragdollBodies)
        {
            rb.isKinematic = false;
        }

        foreach (var col in ragdollColliders)
        {
            col.enabled = true;
        }
    }
    
    public void ShakeRagdoll(Vector3 dragPosition, Vector3 previousDragPosition)
    {
        // Calculate the direction of movement
        Vector3 direction = (dragPosition - previousDragPosition).normalized;

        // Calculate the magnitude of the movement
        float magnitude = (dragPosition - previousDragPosition).magnitude;

        // Apply force to the main rigidbody in the calculated direction
        _mainRb.AddForce(direction * magnitude * 10f, ForceMode.Impulse);
    }

    public void DisableRagdoll()
    {
        if (animator) animator.enabled = true;

        foreach (var rb in ragdollBodies)
        {
            rb.isKinematic = true;
        }

        foreach (var col in ragdollColliders)
        {
            col.enabled = false;
        }
    }
}