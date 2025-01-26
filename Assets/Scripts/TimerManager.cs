using System;
using Events;
using Player;
using ScriptableObject;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private SOFloatVariable timerTotal;
        private float currentTime;
        [SerializeField] private ChangeStateEvent changeStateEvent;
        [SerializeField] private GameListener startTimerListener;
        [SerializeField] private TextMeshProUGUI timerText;
        private bool _shouldTimerRun;
        
        private void Start()
        {
            startTimerListener.Response.AddListener(StartTimer);
        }

        private void StartTimer()
        {
            if (_shouldTimerRun) return;
            _shouldTimerRun = true;
            currentTime = 0;
        }

        private void OnDisable()
        {
            startTimerListener.Response.RemoveListener(StartTimer);
        }

        private void FixedUpdate()
        {
            if (!_shouldTimerRun) return;
            currentTime += Time.deltaTime;
            timerText.text = (timerTotal.value - currentTime).ToString("F2");
            if (currentTime >= timerTotal.value)
            {
                currentTime = 0;
                timerText.text = (timerTotal.value - currentTime).ToString("F2");
                timerText.gameObject.SetActive(false);
                changeStateEvent.Raise(PlayerStateEnum.READY_TO_FLY);
                _shouldTimerRun = false;
            }
        }
    }
}