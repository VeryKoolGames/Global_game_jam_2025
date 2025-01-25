using System.Collections.Generic;
using Events;
using UnityEngine;

public class DecorGenerationManager : MonoBehaviour
{
    [SerializeField] private GameListener onBuildingGeneration;
    [SerializeField] private List<GameObject> buildingPrefabs;
    private Queue<GameObject> pooledBuildings = new Queue<GameObject>();
    [SerializeField] private List<GameObject> activeBuildings = new List<GameObject>();

    private void Start()
    {
        onBuildingGeneration.Response.AddListener(ReplaceExistingBuildings);

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
        List<Transform> oldTransforms = new List<Transform>();
        foreach (var building in activeBuildings)
        {
            oldTransforms.Add(building.transform);
        }

        foreach (var building in activeBuildings)
        {
            ReturnBuildingToPool(building);
        }

        activeBuildings.Clear();

        foreach (var oldTransform in oldTransforms)
        {
            GameObject newBuilding = GetPooledBuilding();
            newBuilding.transform.position = oldTransform.position;
            newBuilding.transform.rotation = oldTransform.rotation;
            newBuilding.transform.localScale = oldTransform.localScale;
            activeBuildings.Add(newBuilding);
        }
    }

    private void OnDisable()
    {
        onBuildingGeneration.Response.RemoveListener(ReplaceExistingBuildings);
    }
}
