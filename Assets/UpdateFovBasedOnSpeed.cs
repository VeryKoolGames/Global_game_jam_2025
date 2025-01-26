using System.Collections;
using DG.Tweening;
using Events;
using ScriptableObject;
using UnityEngine;

public class UpdateFovBasedOnSpeed : MonoBehaviour
{
    [SerializeField] private OnFlyStartListener onFlyStartListener;
    [SerializeField] private GameListener onPlayerDeathEvent;
    [SerializeField] private GameObject player;
    [SerializeField] private GameListener onBoostStartEvent;
    [SerializeField] private SOFloatVariable timeInvincible;
    void Start()
    {
        onFlyStartListener.Response.AddListener(UpdatePov);
        onPlayerDeathEvent.Response.AddListener(SetPovToDeath);
        onBoostStartEvent.Response.AddListener(UpdatePovWhenBoost);
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

    private void UpdatePovWhenBoost()
    {
        StartCoroutine(SetPovForBoost());
    }

    IEnumerator SetPovForBoost()
    {
        float baseFov = Camera.main.fieldOfView;
        Camera.main.DOFieldOfView(100f, 0.5f);
        yield return new WaitForSeconds(timeInvincible.value);
        Camera.main.DOFieldOfView(baseFov, 0.5f);
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
        Vector3 targetRotation = Camera.main.transform.eulerAngles;
        targetRotation.x += 30f; // Add 30 degrees to the current x rotation
        Camera.main.transform.DORotate(targetRotation, 0.5f);
        this.transform.SetParent(player.transform);
    }
    
    private void OnDestroy()
    {
        onFlyStartListener.Response.RemoveListener(UpdatePov);
        onPlayerDeathEvent.Response.RemoveListener(SetPovToDeath);
    }
    
}
