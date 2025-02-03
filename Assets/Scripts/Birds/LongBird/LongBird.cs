using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using DefaultNamespace.GameManager;
using Grid;
using Shaders;
using UnityEngine;

namespace Birds
{
    public class LongBird : Bird
    {
        private Leg _leftLeg, _rightLeg;
        public AnimationClip idleAnimation;
        public AnimationClip leftIdleAnimationLB;
        public AnimationClip rightIdleAnimationLB;
        public AnimationClip leftSwingAnimation;
        public AnimationClip rightSwingAnimation;
        public AnimationClip leftHalfSwingAnimation; 
        public AnimationClip rightHalfSwingAnimation;
        private AnimationClip IDLE;
        private AnimationClip CURR;
        public Vector3 startPos;
        private bool _isLeftJustHit;
        private bool _isSingleForever;
        private Rigidbody2D _rb;
        private bool _spawnStarted;
        private bool moved;
        private bool particlesPlayed;

        public override void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            ShitTimer = shitTime;
            Grid = FindFirstObjectByType<GridManager>();
            _pulseShaderController = GetComponent<PulseShaderController>();
            IsOnHorizontal = 0;
            _leftLeg = transform.GetChild(0).GetComponent<Leg>();
            _leftLeg.IsOnHorizontal = IsOnHorizontal;
            _leftLeg.pos = pos;
            if (IsOnHorizontal == 0)
                _leftLeg.JumpDir = Vector2Int.right;
            else _leftLeg.JumpDir = Vector2Int.up;
            _rightLeg = transform.GetChild(1).GetComponent<Leg>();
            _rightLeg.IsOnHorizontal = IsOnHorizontal;
            if (IsOnHorizontal == 0)
            {
                _rightLeg.pos = pos + Vector2Int.right;
                _rightLeg.JumpDir = Vector2Int.left;
            }
            else
            {
                _rightLeg.pos = pos + Vector2Int.up;
                _rightLeg.JumpDir = Vector2Int.down;
            }
            _leftLeg._isLeft = true;
            _rightLeg._isLeft = false;
            transform.position = startPos;
        }

        private void FixedUpdate()
        {
            if (moved) return;
            transform.position=(_leftLeg.transform.position+_rightLeg.transform.position)/2-new Vector3(0,1.386f,0);
            if (!particlesPlayed)
            {
                particles.Play();
                particlesPlayed = true;
                Animator.Play(idleAnimation.name);
            }
        }

        IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
        {
            Vector3 startPosition = transform.position;
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
        }
        

        public override void OnScare()
        {
            
        }

        public void SwapLegs(bool isLeft, bool isSingle)
        {
            moved = true;
            _isLeftJustHit=isLeft;
            _isSingleForever = isSingle;
            if (isLeft && isSingle)
            {
                Animator.Play(rightHalfSwingAnimation.name);
                CURR=rightHalfSwingAnimation;
                IDLE=rightIdleAnimationLB;
                StartCoroutine(PlayAndWait());
                //transform.position+=Vector3.right;
            }
            else if(!isLeft && isSingle)
            {
                Animator.Play(leftHalfSwingAnimation.name);
                CURR=leftHalfSwingAnimation;
                IDLE=leftIdleAnimationLB;
                StartCoroutine(PlayAndWait());
                //transform.position+=Vector3.left;
            }
            else if (isLeft)
            {
                Animator.Play(rightSwingAnimation.name);
                CURR=rightSwingAnimation;
                IDLE = idleAnimation;
                StartCoroutine(PlayAndWait());
                //transform.position+=Vector3.right;
            }
            else
            {
                Animator.Play(leftSwingAnimation.name);
                CURR=leftSwingAnimation;
                IDLE = idleAnimation;
                StartCoroutine(PlayAndWait());
                //transform.position+=Vector3.left;
            }
            (_leftLeg, _rightLeg) = (_rightLeg, _leftLeg);
            if (IsOnHorizontal == 0)
                _leftLeg.JumpDir = Vector2Int.right;
            else _leftLeg.JumpDir = Vector2Int.up;
            if (IsOnHorizontal == 0)
                _rightLeg.JumpDir = Vector2Int.left;
            else _rightLeg.JumpDir = Vector2Int.down;
            _leftLeg._isLeft = true;
            _rightLeg._isLeft = false;
        }

        private IEnumerator PlayAndWait()
        {
            Animator.Play(CURR.name);
            yield return new WaitForSeconds(CURR.length);
            Animator.Play(IDLE.name);
            if (_isLeftJustHit && !_isSingleForever)
            {
                transform.position+=Vector3.right;
            }
            else if(!_isSingleForever)
            {
                transform.position+=Vector3.left;
            }
        }
        
        public override void Die()
        {
            JustDied = true;
            _leftLeg.Die();
            _rightLeg.Die();
            _rb.gravityScale = 1;
            StartCoroutine(KillLongBird());
        }

        private IEnumerator KillLongBird()
        {
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }
    }
}