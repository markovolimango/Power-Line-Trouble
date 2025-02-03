using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.GameManager
{
    public class ComboMeter : MonoBehaviour
    {
        public int loseTime;
        public TMP_Text comboText;
        private AudioSource _audioSource;
        private int _loseTimer;
        private GameObject _player;
        [NonSerialized] public int Combo;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            comboText.text = "0";
            _loseTimer = 0;
        }

        public void OnBoom()
        {
            if (_loseTimer > 0)
            {
                _loseTimer--;
                return;
            }

            comboText.text = "0";
            Combo = 0;
        }

        public void OnBirdHit()
        {
            Combo++;
            _loseTimer = loseTime;
            comboText.text = Combo.ToString();
        }
    }
}