using System;
using System.Collections;
using Grid;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Birds
{
    public class BabyBird:Bird
    {
        public float timeBetweenSounds;
        public float speedUpRate;
        public AudioClip megaShitSound;
        public float animationSpeedIncrease;
        public ParticleSystem megaShitParticles;
        private camer _camer;
        private float _timer;

        public override void Start()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -5);
            _camer = FindFirstObjectByType<camer>();
            base.Start();
            var emission = megaShitParticles.emission;
            emission.rateOverTime = 0;
            BirdSoundSorce.PlayOneShot(BirdSoundSorce.clip);
            if (Random.Range(0, 2) == 0) Animator.Play(leftIdleAnimation.name);
            else Animator.Play(rightIdleAnimation.name);
        }

        private void FixedUpdate()
        {
            if (JustDied) return;
            _timer += Time.fixedDeltaTime;
            if (_timer >= timeBetweenSounds)
            {
                BirdSoundSorce.PlayOneShot(BirdSoundSorce.clip);
                _timer = 0f;
                timeBetweenSounds *= speedUpRate;
                timeBetweenSounds = Mathf.Max(0.1f, timeBetweenSounds);
            }
        }

        public override void OnTsk()
        {
            Animator.speed += animationSpeedIncrease;
        }

        protected override void Shit()
        {
            print("Ok");
            ExplosionSoundSorce.clip = megaShitSound;
            var emission = megaShitParticles.emission;
            emission.rateOverTime = 200;
            megaShitParticles.Play();
            base.Shit();
            _camer.ShakeIt(3f, 0.3f);
            GetHit();
        }

        public override void OnScare()
        {
            Shit();
        }
    }
}