using System;
using Events;
using UnityEngine;

public class OpenBottleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] bottlesCap;
    [SerializeField] private GameObject[] bottleTrails;
    [SerializeField] private OnFlyStartListener onFlyStartListener;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        onFlyStartListener.Response.AddListener(OpenBottle);
    }

    private void OnDisable()
    {
        onFlyStartListener.Response.RemoveListener(OpenBottle);
    }

    private void OpenBottle(float speed)
    {
        foreach (var cap in bottlesCap)
        {
            cap.SetActive(false);
        }
        foreach (var trail in bottleTrails)
        {
            trail.SetActive(true);
        }
    }
}
