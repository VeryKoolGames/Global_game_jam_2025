using System;
using Events;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private GameEvent PlayerDeathEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit an obstacle.");
            SoundManager.instance.PlaySound(SoundType.Death);
            PlayerDeathEvent.Raise();
        }
    }
}
