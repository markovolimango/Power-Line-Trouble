using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Birds;
using Shaders;
using UnityEngine;

namespace Grid
{
    public abstract class Branch : MonoBehaviour
    {
        public float spaceBetweenBirds;
        public Sprite[] sprites;
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
        }

        private void FixedUpdate()
        {
            ArrangeBirds();
        }

        public virtual void SetEdges(Vector2 start, Vector2 end)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteIndex = 0;
            _spriteRenderer.sprite = sprites[0];
            StartPos = start;
            MidPos = (start + end) / 2;
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
            StartCoroutine(Electrify());
            PulseShaderController.Pulse(3);
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