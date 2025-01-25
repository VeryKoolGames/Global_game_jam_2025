using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameCanvasManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerScoreText;
    public void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 1f);
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetPlayerScore(float score)
    {
        playerScoreText.text = "Total Score: " + score;
    }
}
