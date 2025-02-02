using System;
using System.Collections;
using Grid;
using UnityEngine;
using UnityEngine.Serialization;

namespace Birds
{
    public class BabyBird:Bird
    {
        public float timeBetweenSounds;
        private float _timer = 0f;
        public float speedUpRate;
        public AudioClip megaShitSound;
        public float animationSpeedIncrease;
        public ParticleSystem megaShitParticles;

        public override void Start()
        {
            base.Start();
            var emission = megaShitParticles.emission;
            emission.rateOverTime = 0;
            BirdSoundSorce.PlayOneShot(BirdSoundSorce.clip);
        }

        private void FixedUpdate()
        {
            if(JustDied) return;
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
            megaShitParticles.emissionRate = 1000;
            megaShitParticles.Play();
            GetHit();
        }

        public override void OnScare()
        {
            Shit();
        }
    }
}