using System;
using Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class PowerUpRefill : PowerUp
    {
        private float speed;
        [SerializeField] GameEvent onFuelRefill;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onFuelRefill.Raise();
                particleSystemPowerUp.Play();
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            transform.position += Vector3.back * (speed * Time.deltaTime);
        }
        
        public override void SetSpeed(float speed)
        {
            this.speed = speed / 5;
        }
    }

    public abstract class PowerUp : MonoBehaviour
    {
        [SerializeField] protected ParticleSystem particleSystemPowerUp;
        public abstract void SetSpeed(float speed);
    }
}