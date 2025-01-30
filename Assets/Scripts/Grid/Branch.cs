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
        private int _spriteIndex;
        protected SpriteRenderer _spriteRenderer;
        [NonSerialized] protected List<Bird> Birds = new();
        [NonSerialized] public Vector2 StartPos, MidPos, EndPos;
        protected PulseShaderController PulseShaderController;
        protected Transform Electricity;
        
        private void Start()
        {
            PulseShaderController = GetComponent<PulseShaderController>();
            Electricity = transform.Find("Electricity");
            
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
                MidPos.y -= 0.1f;
            }
        }

        public void DetachBird(Bird bird)
        {
            Birds.Remove(bird);
            if (_spriteIndex > 0)
            {
                _spriteIndex--;
                _spriteRenderer.sprite = sprites[_spriteIndex];
                MidPos.y += 0.1f;
            }
        }

        public void KillBirds(bool leftToRight, float speed)
        {
            //print("KILLING");
            StartCoroutine(Electryfy());
            PulseShaderController.Pulse(3);
            foreach (var bird in Birds.ToList()) bird.GetHit();
        }

        private IEnumerator Electryfy()
        {
            Electricity.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.35f);
            Electricity.gameObject.SetActive(false);
        }

        protected abstract void ArrangeBirds();
    }
}