using System.Collections.Generic;
using Events;
using UnityEngine;

public class DecorGenerationManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacleBuildings = new List<GameObject>();
    
    public void ReplaceExistingBuildings()
    {
        ActivateRandomBuilding();
    }
    
    private void ActivateRandomBuilding()
    {
        foreach (var building in obstacleBuildings)
        {
            if (Random.Range(0f, 1f) <= 0.7f)
            {
                building.SetActive(true);
                building.GetComponent<ObstacleController>().ActivateRandomObstacle();
            }
            else
            {
                building.SetActive(false);
            }
        }
    }

}
