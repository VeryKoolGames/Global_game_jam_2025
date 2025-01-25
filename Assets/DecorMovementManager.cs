using Events;
using UnityEngine;

public class DecorMovementManager : MonoBehaviour
{
    private bool _shouldMove = false;
    private bool _shouldStop = false;
    private float _currentSpeed = 50f;
    private float _decelerationRate = 10f;
    [SerializeField] private OnFlyStartListener onFlyStartListener;
    [SerializeField] private GameListener onPlayerDeathEvent;
    
    private void OnEnable()
    {
        onFlyStartListener.Response.AddListener(StartMoving);
        onPlayerDeathEvent.Response.AddListener(StopMoving);
    }
    
    private void OnDisable()
    {
        onFlyStartListener.Response.RemoveListener(StartMoving);
        onPlayerDeathEvent.Response.RemoveListener(StopMoving);
    }
    
    private void StartMoving(float speed)
    {
        _currentSpeed = speed;
        _shouldMove = true;
        _shouldStop = false;
    }
    
    public void StopMoving()
    {
        _shouldMove = false;
        _shouldStop = true;
    }
    
    void Update()
    {
        if (_shouldMove)
        {
            MoveAlongZAxis();
        }
        else if (_shouldStop)
        {
            SlowDownUntilStops();
        }
    }

    private void MoveAlongZAxis()
    {
        transform.position += Vector3.back * (Time.deltaTime * _currentSpeed);
    }
    
    private void SlowDownUntilStops()
    {
        if (_currentSpeed > 0)
        {
            _currentSpeed -= _decelerationRate * Time.deltaTime * 5;

            transform.position += Vector3.back * (_currentSpeed * Time.deltaTime);
        }
        else
        {
            _currentSpeed = 0f;
            _shouldStop = false;
        }
    }
}
