using UnityEngine;

public class BottlesTrailManager : MonoBehaviour
{
    [SerializeField] private GameObject[] bottleTrail;
    [SerializeField] private float revealSpeed = 1f;
    private MaterialPropertyBlock propertyBlock;
    private float[] revealProgress;

    void Start()
    {
        propertyBlock = new MaterialPropertyBlock();
        revealProgress = new float[bottleTrail.Length];

        for (int i = 0; i < bottleTrail.Length; i++)
        {
            revealProgress[i] = 0;
            UpdateRevealProgress(bottleTrail[i], 0);
        }

        StartCoroutine(RevealBottles());
    }

    private System.Collections.IEnumerator RevealBottles()
    {
        for (int i = 0; i < bottleTrail.Length; i++)
        {
            while (revealProgress[i] < 1f)
            {
                revealProgress[i] += Time.deltaTime * revealSpeed;

                UpdateRevealProgress(bottleTrail[i], revealProgress[i]);

                yield return null;
            }
        }
    }

    private void UpdateRevealProgress(GameObject bottle, float progress)
    {
        Renderer renderer = bottle.GetComponent<Renderer>();
        if (renderer)
        {
            renderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetFloat("_RevealProgress", progress);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}