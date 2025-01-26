using System;
using TMPro;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    void Start()
    {
        highScoreText.text = loadHighScore();
    }
    
    private string loadHighScore()
    {
        string ret = PlayerPrefs.GetFloat("HighScore", 0).ToString();
        ret += "m";
        return ret;
    }
    
}
