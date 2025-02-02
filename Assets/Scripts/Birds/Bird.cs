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
        public GridManager Grid;
        protected int Health = 1;
        protected Branch[,] HorizontalBranches;
        [NonSerialized] public int IsOnHorizontal = -1; //-1 - not set, 0 - vertical, 1 - horizontal
        [NonSerialized] public bool JustDied;
        protected Branch[,] VerticalBranches;
        protected Animator Animator;
        public AnimationClip leftIdleAnimation;
        public AnimationClip rightIdleAnimation;
        public AnimationClip deathAnimation;
        protected PulseShaderController _pulseShaderController;
        public List<AudioClip> birdSounds = new();
        public AudioClip explosionSound;
        protected AudioSource BirdSoundSorce;
        protected AudioSource ExplosionSoundSorce;
        public ParticleSystem particles;

        public virtual void Start()
        {
            BirdSoundSorce = gameObject.AddComponent<AudioSource>();
            ExplosionSoundSorce = gameObject.AddComponent<AudioSource>();
            ExplosionSoundSorce.rolloffMode = AudioRolloffMode.Linear;
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

        public virtual Vector2Int GetRandomPos()
        {
            var random_pos= new Vector2Int(Random.Range(1, Grid.n-1), Random.Range(1, Grid.m-1));
            while(random_pos==pos || random_pos==new Vector2Int(Grid.n-1,Grid.m-1)) random_pos= new Vector2Int(Random.Range(0, Grid.n-1), Random.Range(0, Grid.m-1));
            return random_pos;
            //OVDE BI TREBALO DA BUDE (0,n) tj (0,m) ALI NESTO NE RADI
        }
        
        protected virtual void MoveBirdToPos(Vector2Int newPos)// pazi da newpos bude validan
        {
            
            if (JustDied) return;
            var startPos = transform.position;
            var endPos = new Vector3();
            if (IsOnHorizontal == -1)
            {
                if (newPos.y == Grid.m - 1) IsOnHorizontal = 1;
                else if (newPos.x == Grid.n - 1) IsOnHorizontal = 0;
                else
                {
                    if (Random.Range(0, 2) == 0) IsOnHorizontal = 1;
                    else  IsOnHorizontal = 0;
                }
                /*/
                if (Random.Range(0, 2) == 0) IsOnHorizontal = 1;
                else  IsOnHorizontal = 0;
                /*/
            }
            
            print(newPos);

            if (IsOnHorizontal == 1)
                endPos = HorizontalBranches[newPos.y, newPos.x].MidPos;
            else if (IsOnHorizontal == 0) endPos = VerticalBranches[newPos.y, newPos.x].MidPos;
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

        protected void MoveInSmoothSlurpeLine(Vector2 start, Vector2 end, float height, float duration)
        {
            StartCoroutine(MoveInSmoothSlurpeLineCoroutine(start, end, (end - start).magnitude * jumpHeightFactor,
                jumpDuration));
        }

        protected IEnumerator MoveInSmoothSlurpeLineCoroutine(Vector2 start, Vector2 end, float height, float duration)
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
        
        public virtual void OnScare()
        {
            particles.Play();
            var newPos = GetRandomPos();
            MoveBirdToPos(newPos);
        }

        public virtual void GetHit()
        {
            birdHit.Invoke();
            if (JustDied) return;
            particles.Play();
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

        protected virtual IEnumerator PlayDeathAnimation()
        {
            ExplosionSoundSorce.Play();
            if (Random.Range(0, 2)==0) BirdSoundSorce.Play();
            Animator.Play(deathAnimation.name);
            yield return new WaitForSeconds(Mathf.Max(Mathf.Max(BirdSoundSorce.clip.length, ExplosionSoundSorce.clip.length),deathAnimation.length));
            Destroy(gameObject);
        }
    }
}