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
    [SerializeField] private GameListener onPlayerDeathEvent;

    private float gameSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        onFlyStartListener.Response.AddListener(StartSpawningPowerUp);
        onPlayerDeathEvent.Response.AddListener(OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        _shouldSpawnPowerUp = false;
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
        Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), transform.position.y, transform.position.z);
        GameObject newObj = Instantiate(powerUp, spawnPosition, Quaternion.identity);
        newObj.GetComponent<PowerUp>().SetSpeed(gameSpeed);
    }
}
