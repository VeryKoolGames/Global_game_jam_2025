using UnityEngine;

public class ragdollTEST : MonoBehaviour
{
    public Rigidbody rb;         // Le Rigidbody auquel appliquer la force
    public Transform target;     // La cible vers laquelle appliquer la force
    public float forceAmount = 10f; // L'intensité de la force

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    void FixedUpdate()
    {
        if (target != null && rb != null)
        {
            Vector3 direction = (target.position - rb.position).normalized;

            rb.linearVelocity = direction * forceAmount;
        }
    }
}
