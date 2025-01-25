using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    private Rigidbody[] ragdollBodies;
    private Collider[] ragdollColliders;
    private Animator animator;
    public Rigidbody[] _rbToApplyForce;

    private void Start()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        animator = GetComponent<Animator>();

        DisableRagdoll();
        EnableZConstraint();
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
        Debug.Log("Shake Ragdoll");
        Vector3 direction = (dragPosition - previousDragPosition).normalized;

        float magnitude = (dragPosition - previousDragPosition).magnitude;
        
        foreach (var rb in _rbToApplyForce)
        {
            rb.AddForce(direction * magnitude * 50f, ForceMode.Impulse);
        }
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

    public void RemoveYConstraint()
    {
        foreach (var rb in ragdollBodies)
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        }
    }
    
    private void EnableZConstraint()
    {
        foreach (var rb in ragdollBodies)
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionZ;
        }
    }
    
    public void EnableYConstraint()
    {
        foreach (var rb in ragdollBodies)
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionY;
        }
    }
}