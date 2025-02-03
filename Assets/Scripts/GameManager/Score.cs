using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.GameManager
{
    public class Score : MonoBehaviour
    {
        [NonSerialized] public int _score = 0;
        private int _addedScore=0;
        public TMP_Text scoreText;
        public TMP_Text addedScoreText;
        private ScoreTransfer _st;

        private void Start()
        {
            _st = GetComponent<ScoreTransfer>();
        }

        private void Update()
        {
            scoreText.text = _score.ToString();
            ScoreTransfer.SetScore(_score);
            if (_addedScore > 0)
                addedScoreText.text = "+ " + _addedScore;
            else
                addedScoreText.text = "";
        }

        public void AddScore(int score)
        {
            _addedScore = score;
        }

        public void OnTsk()
        {
            _score += _addedScore;
            _addedScore = 0;
        }
    }
}