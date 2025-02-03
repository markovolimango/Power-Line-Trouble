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

        public override void Start()
        {
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

        public override void OnScare()
        {
            
        }

        public void SwapLegs(bool isLeft, bool isSingle)
        {
            _isLeftJustHit=isLeft;
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
            if (_isLeftJustHit)
            {
                transform.position+=Vector3.right;
            }
            else
            {
                transform.position+=Vector3.left;
            }
        }
        
        public override void Die()
        {
            JustDied = true;
            _leftLeg.Die();
            _rightLeg.Die();
            Destroy(gameObject);
        }
    }
}