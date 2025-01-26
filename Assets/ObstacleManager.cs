using System;
using Events;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private GameEvent PlayerDeathEvent;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Obstacle hit something.");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit an obstacle.");
            SoundManager.instance.PlaySound(SoundType.Death);
            PlayerDeathEvent.Raise();
        }
    }
}
