using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameCanvasManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerScoreText;
    public Animator animator;

    public void ReplayGame()
    {
        StartCoroutine(Replay());
        Debug.Log("Replay");

    }

    public void QuitGame()
    {
        StartCoroutine(Quit());

    }

    IEnumerator Replay()
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Quit()
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }






    public void SetPlayerScore(float score)
    {
        playerScoreText.text = "" + score;
    }


}
