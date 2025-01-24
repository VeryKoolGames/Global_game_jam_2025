using System;
using UnityEngine;

namespace Player
{
    public class ScoreManager : MonoBehaviour
    {
        private float score = 0;

        public void Update()
        {
            score += Time.deltaTime;
        }
    }
}