using System;
using System.Collections;
using Events;
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
                Destroy(gameObject);
            }
        }

        private IEnumerator makePlayerInvincible(GameObject player)
        {
            player.tag = "";
            yield return new WaitForSeconds(timeInvincible.value);
            player.tag = "Player";
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
}