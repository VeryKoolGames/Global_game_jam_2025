using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private GameObject[] Obstacles;
    [SerializeField] private Material[] buildingMaterials;
    
    public void ActivateRandomObstacle()
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
