using System;
using System.Collections;
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
        [SerializeField] private GameListener onPlayerDeathEvent;
        [SerializeField] private EndGameCanvasManager endGameCanvas;

        public void FixedUpdate()
        {
            if (!shouldCountScore) return;
            score += Time.deltaTime * playerSpeed;
            scoreText.text = (int)score + "m";
        }
        
        private void OnPlayerDeath()
        {
            playerSpeed = 0;
            shouldCountScore = false;
            StartCoroutine(WaitBeforeOpeningCanvas());
        }

        private IEnumerator WaitBeforeOpeningCanvas()
        {
            yield return new WaitForSeconds(5);
            endGameCanvas.gameObject.SetActive(true);
            endGameCanvas.SetPlayerScore(score);
        }
        
        private void StartCountingScore(float speed)
        {
            shouldCountScore = true;
            playerSpeed = speed;
        }
        
        private void OnEnable()
        {
            onFlyStartListener.Response.AddListener(StartCountingScore);
            onPlayerDeathEvent.Response.AddListener(OnPlayerDeath);
        }
        
        private void OnDisable()
        {
            onFlyStartListener.Response.RemoveListener(StartCountingScore);
            onPlayerDeathEvent.Response.RemoveListener(OnPlayerDeath);
        }
    }
}