using System;
using Events;
using UnityEngine;

public class DecorSpawnManager : MonoBehaviour
{

    [SerializeField] private Transform targetTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Decor"))
        {
            other.gameObject.transform.parent.position = targetTransform.position;
            other.GetComponentInParent<DecorGenerationManager>().ReplaceExistingBuildings();
        }
    }
    
}
