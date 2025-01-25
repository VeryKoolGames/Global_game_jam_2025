using System;
using Events;
using UnityEngine;

public class CanonPlateformMover : MonoBehaviour
{
    [SerializeField] private OnFlyStartListener onFlyStartListener;
    private bool shouldMove = false;

    private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        onFlyStartListener.Response.AddListener(StartMovingCanonPlateform);
    }

    private void OnDisable()
    {
        onFlyStartListener.Response.RemoveListener(StartMovingCanonPlateform);
    }

    private void Update()
    {
        if (!shouldMove) return;
        transform.position += Vector3.back * speed * Time.deltaTime;
    }

    private void StartMovingCanonPlateform(float speed)
    {
        shouldMove = true;
        this.speed = speed;
    }
}
