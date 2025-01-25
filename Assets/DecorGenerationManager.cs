using System.Collections.Generic;
using Events;
using UnityEngine;

public class DecorGenerationManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> buildingPrefabs;
    private Queue<GameObject> pooledBuildings = new Queue<GameObject>();
    [SerializeField] private List<GameObject> activeBuildings = new List<GameObject>();
    [SerializeField] private GameObject[] Obstacles;
    [SerializeField] private Material[] buildingMaterials;

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        foreach (var prefab in buildingPrefabs)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject building = Instantiate(prefab, transform);
                building.SetActive(false);
                pooledBuildings.Enqueue(building);
            }
        }
    }

    private GameObject GetPooledBuilding()
    {
        if (pooledBuildings.Count > 0)
        {
            GameObject building = pooledBuildings.Dequeue();
            building.SetActive(true);
            return building;
        }

        GameObject newBuilding = Instantiate(buildingPrefabs[Random.Range(0, buildingPrefabs.Count)], this.transform);
        return newBuilding;
    }

    private void ReturnBuildingToPool(GameObject building)
    {
        building.SetActive(false);
        pooledBuildings.Enqueue(building);
    }

    public void ReplaceExistingBuildings()
    {
        ActivateRandomObstacle();
    }
    
    private void ActivateRandomObstacle()
    {
        foreach (var obst in Obstacles)
        {
            obst.SetActive(false);
            ApplyRandomMaterial(obst);
        }
        Obstacles[Random.Range(0, Obstacles.Length)].SetActive(true);
        if (Random.Range(0f, 1f) <= 0.5f)
        {
            Obstacles[Random.Range(0, Obstacles.Length)].SetActive(true);
        }
    }
    
    private void ApplyRandomMaterial(GameObject building)
    {
        building.GetComponent<MeshRenderer>().material = buildingMaterials[Random.Range(0, buildingMaterials.Length)];
    }

}
