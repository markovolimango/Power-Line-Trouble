using System.Collections;
using UnityEngine;

namespace Birds
{
    public class Borb : Bird
    {
        private static readonly int NoiseSpeed = Shader.PropertyToID("NoiseSpeed");
        public AnimationClip hitLeftIdleAnimation;
        public AnimationClip hitRightIdleAnimation;

        protected Transform Electricity;
        protected Animator ElectricityAnimator;
        protected bool IsHit;

        public override void Start()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            base.Start();
            Health = 2;
            Electricity = transform.Find("Electricity");
            Electricity.gameObject.SetActive(false);
            ElectricityAnimator = Electricity.GetComponent<Animator>();
            IsHit = false;
            if (Random.Range(0, 2) == 0) Animator.Play(leftIdleAnimation.name);
            else Animator.Play(rightIdleAnimation.name);
        }

        public override void OnBoom()
        {
            base.OnBoom();
            if (IsHit && !JustDied && Random.Range(0, 4) == 0) StartCoroutine(Electryfy());
        }

        private IEnumerator Electryfy()
        {
            Electricity.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            Electricity.gameObject.SetActive(false);
        }

        public override void GetHit()
        {
            base.GetHit();
            if (!JustDied)
            {
                BirdSoundSorce.Play();
                IsHit = true;
                IsOnHorizontal = -1;
                leftIdleAnimation = hitLeftIdleAnimation;
                rightIdleAnimation = hitRightIdleAnimation;
                var newPos = GetRandomPos();
                MoveBirdToPos(newPos);

                if (Random.Range(0, 2) == 0)
                {
                    Animator.Play(leftIdleAnimation.name);
                    ElectricityAnimator.Play(leftIdleAnimation.name);
                    //Electricity.GetComponent<SpriteRenderer>().sharedMaterial.SetVector("NoiseSpeed", new Vector2(0.1f,Random.Range(-10f,0f)));
                    //print("Lidle");
                }
                else
                {
                    Animator.Play(rightIdleAnimation.name);
                    ElectricityAnimator.Play(rightIdleAnimation.name);
                    //Electricity.GetComponent<SpriteRenderer>().sharedMaterial.SetVector("NoiseSpeed", new Vector2(0.1f,Random.Range(-10f,0f)));
                    //print("Ridle");
                }

                ShitTimer = shitTime;
            }
        }
    }
}