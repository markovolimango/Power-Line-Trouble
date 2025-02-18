using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Birds;
using DefaultNamespace.GameManager;
using Shaders;
using UnityEngine;

namespace Grid
{
    public abstract class Branch : MonoBehaviour
    {
        public float spaceBetweenBirds;
        public Sprite[] sprites;
        public Score Score;
        public ComboMeter comboMeter;
        private camer _camer;
        private Transform _electricity;
        private int _spriteIndex;
        private SpriteRenderer _spriteRenderer, _electricitySpriteRenderer;
        [NonSerialized] protected List<Bird> Birds = new();
        protected PulseShaderController PulseShaderController;
        [NonSerialized] public Vector2 StartPos, MidPos, EndPos;

        private void Start()
        {
            PulseShaderController = GetComponent<PulseShaderController>();
            _electricity = transform.Find("Electricity");
            _electricitySpriteRenderer = _electricity.GetComponent<SpriteRenderer>();
            _electricity = transform.Find("Electricity");
            _electricity.gameObject.SetActive(false);
            Score = GameObject.FindGameObjectWithTag("Car").GetComponent<Score>();
            comboMeter = GameObject.FindGameObjectWithTag("Car").GetComponent<ComboMeter>();
            _camer = FindFirstObjectByType<camer>();
        }

        private void FixedUpdate()
        {
            ArrangeBirds();
        }

        public void ScareBirds()
        {
            foreach (var bird in Birds.ToList()) bird.OnScare();
        }

        public virtual void SetEdges(Vector2 start, Vector2 end)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteIndex = 0;
            _spriteRenderer.sprite = sprites[0];
            StartPos = start;
            MidPos = (start + end) / 2;
            MidPos += Vector2.down * 0.05f;
            EndPos = end;
        }

        public virtual void AttachBird(Bird bird)
        {
            Birds.Add(bird);
            if (_spriteIndex < sprites.Length - 1)
            {
                _spriteIndex++;
                _spriteRenderer.sprite = sprites[_spriteIndex];
                _electricitySpriteRenderer.sprite = sprites[_spriteIndex];
                MidPos.y -= 0.05f;
            }
        }

        public void DetachBird(Bird bird)
        {
            Birds.Remove(bird);
            if (_spriteIndex > 0)
            {
                _spriteIndex--;
                _spriteRenderer.sprite = sprites[_spriteIndex];
                _electricitySpriteRenderer.sprite = sprites[_spriteIndex];
                MidPos.y += 0.05f;
            }
        }

        public void KillBirds(bool leftToRight, float speed)
        {
            //print("KILLING");
            _camer.ShakeIt(Birds.Count * 0.5f, 0.1f);
            StartCoroutine(Electrify());
            PulseShaderController.Pulse(3);
            var scoreToAdd = 0;
            foreach (var bird in Birds.ToList()) scoreToAdd += bird.scoreIncrease;
            Score.AddScore((scoreToAdd + comboMeter.Combo) * Birds.Count);
            foreach (var bird in Birds.ToList()) bird.GetHit();
        }

        private IEnumerator Electrify()
        {
            _electricity.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _electricity.gameObject.SetActive(false);
        }

        protected abstract void ArrangeBirds();
    }
}