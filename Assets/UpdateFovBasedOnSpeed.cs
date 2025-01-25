using DG.Tweening;
using Events;
using UnityEngine;

public class UpdateFovBasedOnSpeed : MonoBehaviour
{
    [SerializeField] private OnFlyStartListener onFlyStartListener;
    void Start()
    {
        onFlyStartListener.Response.AddListener(UpdatePov);
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
    
}
