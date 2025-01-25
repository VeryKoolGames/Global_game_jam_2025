using System.Collections.Generic;
using DefaultNamespace;
using Events;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> powerUpPrefabs;
    private float spawnDelay;
    [SerializeField] private OnFlyStartListener onFlyStartListener;
    private bool _shouldSpawnPowerUp = false;

    private float gameSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        onFlyStartListener.Response.AddListener(StartSpawningPowerUp);
    }
    
    private void OnDisable()
    {
        onFlyStartListener.Response.RemoveListener(StartSpawningPowerUp);
    }
    
    private void StartSpawningPowerUp(float speed)
    {
        gameSpeed = speed;
        _shouldSpawnPowerUp = true;
    }

    void Update()
    {
        if (_shouldSpawnPowerUp)
        {
            spawnDelay -= Time.deltaTime;
            if (spawnDelay <= 0)
            {
                SpawnPowerUp();
                spawnDelay = Random.Range(5, 10);
            }
        }
    }
    
    private void SpawnPowerUp()
    {
        GameObject powerUp = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Count)];
        GameObject newObj = Instantiate(powerUp, transform.position, Quaternion.identity);
        newObj.GetComponent<PowerUp>().SetSpeed(gameSpeed);
    }
}
