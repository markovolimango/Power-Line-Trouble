using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

namespace DefaultNamespace.GameManager
{
    public class Health : MonoBehaviour
    {
        public static int _hp;
        public int maxhp;
        public List<Sprite> sprites;
        public int hpToSpriteChange;
        public Slider Slider;
        public Gradient Gradient;
        public Image Fill;

        private SpriteRenderer _spriteRenderer;
        //public RedCornersShaderController redCorners;


        private void Start()
        {
            SetMaxHealth(maxhp);
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = sprites[0];
            //redCorners = FindFirstObjectByType<RedCornersShaderController>();
            //redCorners.PlayPulseEffect( 3f);
        }

        public void SetMaxHealth(int health)
        {
            Slider.maxValue = health;
            Slider.value = health;
            Fill.color = Gradient.Evaluate(1f);
            _hp = maxhp;
        }

        public void Damage(int amount)
        {
            _hp -= amount;
            SetHealth();
            if (_hp <= 0) GameOver();
        }

        private void GameOver()
        {
            SceneManager.LoadScene(2);
        }

        public void Heal(int amount)
        {
            _hp = Math.Min(_hp + amount, maxhp);
            SetHealth();
        }

        private void SetHealth()
        {
            Slider.value = _hp;
            Fill.color = Gradient.Evaluate(Slider.normalizedValue);
            _spriteRenderer.sprite = sprites[(maxhp - _hp) / 15];
        }
    }
}