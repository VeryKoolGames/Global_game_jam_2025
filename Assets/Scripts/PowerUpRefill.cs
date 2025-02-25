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
            if (other.CompareTag("Player") || other.CompareTag("PlayerInvincible"))
            {
                SoundManager.instance.PlaySound(SoundType.REFILL);
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
            this.speed = speed;
        }
    }

    public abstract class PowerUp : MonoBehaviour
    {
        [SerializeField] protected ParticleSystem particleSystemPowerUp;
        public abstract void SetSpeed(float speed);
    }
}