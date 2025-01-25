using System;
using UnityEngine;

public class DecorSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] decorRightPrefabs;
    [SerializeField] GameObject[] decorLeftPrefabs;
    [SerializeField] GameObject[] decorBehindPrefabs;
    [SerializeField] Transform[] decorRightSpawnPoints;
    [SerializeField] Transform[] decorLeftSpawnPoints;
    [SerializeField] Transform[] decorBehindSpawnPoints;

    [SerializeField] private Transform targetTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Decor"))
        {
            other.gameObject.transform.parent.position = targetTransform.position;
        }
    }

    private void SpawnNewDecor()
    {
        for (int i = 0; i < decorRightSpawnPoints.Length; i++)
        {
            Instantiate(decorRightPrefabs[UnityEngine.Random.Range(0, decorRightPrefabs.Length)], decorRightSpawnPoints[i].position, Quaternion.identity);
        }
        for (int i = 0; i < decorLeftSpawnPoints.Length; i++)
        {
            Instantiate(decorLeftPrefabs[UnityEngine.Random.Range(0, decorLeftPrefabs.Length)], decorLeftSpawnPoints[i].position, Quaternion.identity);
        }
    }
}
