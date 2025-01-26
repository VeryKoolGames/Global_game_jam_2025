using System.Collections;
using ScriptableObject;
using UnityEngine;

public class PlayerInvincibilityManager : MonoBehaviour
{
    [SerializeField] private SOFloatVariable timeInvincible;
    public void MakePlayerInvincible()
    {
        StartCoroutine(makePlayerInvincible(gameObject));
    }
        
    private IEnumerator makePlayerInvincible(GameObject player)
    {
        player.tag = "";
        yield return new WaitForSeconds(timeInvincible.value);
        player.tag = "Player";
    }
}
