using System;
using TMPro;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScoreText.text = loadHighScore();
    }
    
    private string loadHighScore()
    {
        string ret = PlayerPrefs.GetString("HighScore", "0");
        ret += "m";
        return ret;
    }
    
}
