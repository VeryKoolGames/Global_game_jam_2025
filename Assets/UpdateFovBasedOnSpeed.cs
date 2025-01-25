using DG.Tweening;
using Events;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class UpdateFovBasedOnSpeed : MonoBehaviour
{
    [SerializeField] private OnFlyStartListener onFlyStartListener;
    [SerializeField] private GameListener onPlayerDeathEvent;
    [SerializeField] private GameObject player;
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
        UpdateCameraZoom();
        Camera.main.DOFieldOfView(targetFov, 0.5f);
        
    }

    private void UpdateCameraZoom()
    {
        UnityEngine.Vector3 cameraPosition = Camera.main.transform.position;
        cameraPosition.z = -6.5f;
        gameObject.transform.DOMove(cameraPosition, 0.5f);
    }
    
    private void SetPovToDeath()
    {
        Camera.main.DOFieldOfView(100f, 0.5f);
        this.transform.SetParent(player.transform);
    }
    
    private void OnDestroy()
    {
        onFlyStartListener.Response.RemoveListener(UpdatePov);
        onPlayerDeathEvent.Response.RemoveListener(SetPovToDeath);
    }
    
}
