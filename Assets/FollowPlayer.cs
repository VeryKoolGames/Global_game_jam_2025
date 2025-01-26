using System;
using DG.Tweening;
using Events;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float followDuration = 0.5f;
    [SerializeField] private Vector3 offset;
    private bool _isFollowing;
    [SerializeField] private GameListener stopShakeEvent;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned!");
        }
    }
    
    private void OnEnable()
    {
        stopShakeEvent.Response.AddListener(StartFollow);
    }

    private void StartFollow()
    {
        _isFollowing = true;
    }

    private void Update()
    {
        if (player != null && _isFollowing)
        {
            transform.DOMove(player.position + offset, followDuration).SetEase(Ease.Linear);
        }
    }

    private void OnDestroy()
    {
        stopShakeEvent.Response.RemoveListener(StartFollow);
    }
    
    public void StopFollowing()
    {
        _isFollowing = false;
    }
}
