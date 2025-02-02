using System;
using UnityEngine;
using TMPro;

namespace DefaultNamespace.GameManager
{
    public class Score: MonoBehaviour
    {
        [NonSerialized] public int _score = 100;
        private int _addedScore=0;
        public TMP_Text scoreText;
        public TMP_Text addedScoreText;
        
        public void AddScore(int score)
        {
            _addedScore = score;
        }

        public void OnTsk()
        {
            _score+=_addedScore;
            _addedScore = 0;
        }
        
        private void Update()
        {
            scoreText.text = _score.ToString();
            if (_addedScore > 0)
            {
                addedScoreText.text = "+ " + _addedScore.ToString();
            }
            else
            {
                addedScoreText.text = "";
            }
        }
    }
}