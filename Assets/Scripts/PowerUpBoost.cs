using System;
using System.Collections;
using Events;
using Player;
using ScriptableObject;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class PowerUpBoost : PowerUp
    {
        private float speed;
        [SerializeField] GameEvent onBoostEvent;
        [SerializeField] private SOFloatVariable timeInvincible;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onBoostEvent.Raise();
                particleSystemPowerUp.Play();
                other.GetComponent<PlayerInvincibilityManager>().MakePlayerInvincible();
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
}