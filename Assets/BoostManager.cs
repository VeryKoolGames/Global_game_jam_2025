using System;
using System.Collections;
using Events;
using ScriptableObject;
using UnityEngine;
using UnityEngine.UI;

public class BoostManager : MonoBehaviour
{
    [SerializeField] private GameObject boostEffect;
    [SerializeField] private GameListener playerDeathEvent;
    [SerializeField] private OnFlyStartListener onFlyStartEvent;
    [SerializeField] private GameListener onBoostListener;
    [SerializeField] private SOFloatVariable timeInvincible;
    private float baseAlpha;
    private Color baseColor;
    
    private Material material;

    void Start()
    {
        playerDeathEvent.Response.AddListener(OnPlayerDeath);
        onFlyStartEvent.Response.AddListener(OnFlyStart);
        onBoostListener.Response.AddListener(IncreaseEffect);
        material = boostEffect.GetComponent<RawImage>().material;
        baseAlpha = material.GetFloat("_Alpha");
        baseColor = material.color;
    }

    private void OnFlyStart(float speed)
    {
        boostEffect.SetActive(true);
    }
    
    private void OnPlayerDeath()
    {
        boostEffect.SetActive(false);
    }

    private void IncreaseEffect()
    { 
        StartCoroutine(applyBoostEffect());
    }

    private IEnumerator applyBoostEffect()
    {
        material.color = Color.green;
        material.SetFloat("_Alpha", baseAlpha + 50);
        yield return new WaitForSeconds(timeInvincible.value);
        material.color = baseColor;
        material.SetFloat("_Alpha", baseAlpha);
    }

    private void OnDestroy()
    {
        playerDeathEvent.Response.RemoveListener(OnPlayerDeath);
        onFlyStartEvent.Response.RemoveListener(OnFlyStart);
        onBoostListener.Response.RemoveListener(IncreaseEffect);
        material.color = baseColor;
        material.SetFloat("_Alpha", baseAlpha);
    }
}
