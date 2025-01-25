using System;
using Events;
using TMPro;
using UnityEngine;

namespace Player
{
    public class ScoreManager : MonoBehaviour
    {
        private float score = 0;
        private float playerSpeed;
        private bool shouldCountScore = false;
        [SerializeField] private OnFlyStartListener onFlyStartListener;
        [SerializeField] private TextMeshProUGUI scoreText;

        public void Update()
        {
            if (!shouldCountScore) return;
            score += Time.deltaTime * playerSpeed;
            scoreText.text = "Score: " + (int)score;
        }
        
        private void StartCountingScore(float speed)
        {
            shouldCountScore = true;
            playerSpeed = speed;
        }
        
        private void OnEnable()
        {
            onFlyStartListener.Response.AddListener(StartCountingScore);
        }
        
        private void OnDisable()
        {
            onFlyStartListener.Response.RemoveListener(StartCountingScore);
        }
    }
}