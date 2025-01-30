using System;
using System.Collections;
using System.Collections.Generic;
using Grid;
using Shaders;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Birds
{
    public abstract class Bird : MonoBehaviour
    {
        public GameObject shit;
        public int shitTime;
        public float jumpHeightFactor;
        public float jumpDuration;
        public Vector2Int pos;
        public UnityEvent birdHit;
        protected int ShitTimer;
        protected GridManager Grid;
        protected int Health = 1;
        protected Branch[,] HorizontalBranches;
        [NonSerialized] public int IsOnHorizontal = -1; //-1 - not set, 0 - vertical, 1 - horizontal
        [NonSerialized] public bool JustDied;
        protected Branch[,] VerticalBranches;
        protected Animator Animator;
        public AnimationClip leftIdleAnimation;
        public AnimationClip rightIdleAnimation;
        public AnimationClip leftFlyingAnimation;
        public AnimationClip rightFlyingAnimation;
        public AnimationClip deathAnimation;
        protected PulseShaderController _pulseShaderController;
        public List<AudioClip> birdSounds = new();
        public AudioClip explosionSound;
        protected AudioSource BirdSoundSorce;
        protected AudioSource ExplosionSoundSorce;

        protected virtual void Start()
        {
            BirdSoundSorce = gameObject.AddComponent<AudioSource>();
            ExplosionSoundSorce = gameObject.AddComponent<AudioSource>();
            BirdSoundSorce.clip=birdSounds[Random.Range(0,birdSounds.Count)];
            ExplosionSoundSorce.clip=explosionSound;
            _pulseShaderController = GetComponent<PulseShaderController>();
            Animator = GetComponent<Animator>();
            Grid = FindFirstObjectByType<GridManager>();
            HorizontalBranches = Grid.HorizontalBranches;
            VerticalBranches = Grid.VerticalBranches;
            var startingPos = pos;
            ShitTimer = shitTime;
            pos = new Vector2Int(-1, -1);
            MoveBirdToPos(startingPos);
            if(Random.Range(0,2)==0) Animator.Play(leftIdleAnimation.name);
            else Animator.Play(rightIdleAnimation.name);
            Animator.Play(leftIdleAnimation.name);
        }

        protected virtual void MoveBirdToPos(Vector2Int newPos)
        {
            
            if (JustDied) return;
            var startPos = transform.position;
            var endPos = new Vector3();
            if (IsOnHorizontal == -1)
            {
                if (Random.Range(0, 2) == 0)
                    IsOnHorizontal = 1;
                else
                    IsOnHorizontal = 0;
            }

            if (IsOnHorizontal == 1)
                endPos = HorizontalBranches[newPos.y, newPos.x].MidPos;
            else if (IsOnHorizontal == 0) endPos = VerticalBranches[newPos.y, newPos.x].MidPos;
            if(startPos.x<endPos.x) Animator.Play(rightFlyingAnimation.name);
            else Animator.Play(leftFlyingAnimation.name);
            MoveInSmoothSlurpeLine(startPos, endPos, 0.5f, 0.2f);
            if (IsOnHorizontal == 1)
            {
                if (pos != new Vector2Int(-1, -1)) HorizontalBranches[pos.y, pos.x].DetachBird(this);
                HorizontalBranches[newPos.y, newPos.x].AttachBird(this);
                transform.position = new Vector3(transform.position.x, transform.position.y, Grid.m - pos.y - 0.5f);
            }
            else if (IsOnHorizontal == 0)
            {
                if (pos != new Vector2Int(-1, -1)) VerticalBranches[pos.y, pos.x].DetachBird(this);
                VerticalBranches[newPos.y, newPos.x].AttachBird(this);
                transform.position = new Vector3(transform.position.x, transform.position.y, Grid.m - pos.y - 0.5f);
            }

            pos = newPos;
        }

        public void MoveInSmoothSlurpeLine(Vector2 start, Vector2 end, float height, float duration)
        {
            StartCoroutine(MoveInSmoothSlurpeLineCoroutine(start, end, (end - start).magnitude * jumpHeightFactor,
                jumpDuration));
        }

        private IEnumerator MoveInSmoothSlurpeLineCoroutine(Vector2 start, Vector2 end, float height, float duration)
        {
            var elapsedTime = 0f;
            var controlPoint = (start + end) / 2 + Vector2.up * height;

            while (elapsedTime < duration)
            {
                var t = elapsedTime / duration;
                t = Mathf.SmoothStep(0f, 1f, t);
                var position = CalculateBezierPoint(t, start, controlPoint, end);
                transform.position = position;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = end;
        }

        private static Vector2 CalculateBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2)
        {
            var u = 1 - t;
            var tt = t * t;
            var uu = u * u;
            var p = uu * p0; // (1-t)^2 * p0
            p += 2 * u * t * p1; // 2(1-t)t * p1
            p += tt * p2; // t^2 * p2
            return p;
        }


        public virtual void OnTsk()
        {
        }

        public virtual void OnBoom()
        {
            if (JustDied) return;
            if (ShitTimer > 0)
            {
                ShitTimer--;
                if(ShitTimer<=2) _pulseShaderController.Pulse(1);
                return;
            }

            Shit();
        }

        protected virtual void Shit()
        {
            Instantiate(shit, new Vector3(transform.position.x, transform.position.y, 10f), transform.rotation);
            ShitTimer = shitTime;
        }

        public virtual void GetHit()
        {
            birdHit.Invoke();
            if (JustDied) return;
            Health--;
            if (IsOnHorizontal == 1)
                HorizontalBranches[pos.y, pos.x].DetachBird(this);
            else if (IsOnHorizontal == 0) VerticalBranches[pos.y, pos.x].DetachBird(this);

            if (Health <= 0) Die();
        }

        public virtual void Die()
        {
            JustDied = true;
            StartCoroutine(PlayDeathAnimation());
        }

        private IEnumerator PlayDeathAnimation()
        {
            ExplosionSoundSorce.Play();
            if (Random.Range(0, 2)==0) BirdSoundSorce.Play();
            Animator.Play(deathAnimation.name);
            yield return new WaitForSeconds(Mathf.Max(Mathf.Max(BirdSoundSorce.clip.length, ExplosionSoundSorce.clip.length),deathAnimation.length));
            Destroy(gameObject);
        }
    }
}