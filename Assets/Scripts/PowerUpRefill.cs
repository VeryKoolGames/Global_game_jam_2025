using System;
using Events;
using UnityEngine;

namespace DefaultNamespace
{
    public class PowerUpRefill : MonoBehaviour
    {
        GameEvent onFuelRefill;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onFuelRefill.Raise();
                Destroy(gameObject);
            }
        }
    }
}