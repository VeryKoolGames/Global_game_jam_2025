using DG.Tweening;
using Events;
using UnityEngine;

public class UpdateFovBasedOnSpeed : MonoBehaviour
{
    [SerializeField] private OnFlyStartListener onFlyStartListener;
    [SerializeField] private GameListener onPlayerDeathEvent;
    void Start()
    {
        onFlyStartListener.Response.AddListener(UpdatePov);
        onPlayerDeathEvent.Response.AddListener(SetPovToDeath);
    }

    private void UpdatePov(float speed)
    {
        float baseFov = 50f;
        float maxFov = 70f;

        float scalePov = Mathf.Clamp(speed / 2, 0f, 20f);
        float targetFov = Mathf.Clamp(baseFov + scalePov, baseFov, maxFov);
        Debug.Log("targetFov: " + targetFov);
        Camera.main.DOFieldOfView(targetFov, 0.5f);
    }
    
    private void SetPovToDeath()
    {
        Camera.main.DOFieldOfView(100f, 0.5f);
    }
    
    private void OnDisable()
    {
        onFlyStartListener.Response.RemoveListener(UpdatePov);
        onPlayerDeathEvent.Response.RemoveListener(SetPovToDeath);
    }
    
}
