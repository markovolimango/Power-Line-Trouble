using System.Collections;
using Grid;
using UnityEngine;

namespace Birds
{
    public class Borb : Bird
    {
        private static readonly int NoiseSpeed = Shader.PropertyToID("NoiseSpeed");

        protected Transform Electricity;
        protected Animator ElectricityAnimator;
        protected bool IsHit;
        public AnimationClip hitLeftIdleAnimation;
        public AnimationClip hitRightIdleAnimation;
        protected override void Start()
        {
            base.Start();
            Health = 2;
            Electricity = transform.Find("Electricity");
            Electricity.gameObject.SetActive(false);
            ElectricityAnimator=Electricity.GetComponent<Animator>();
            IsHit = false;
        }

        public override void OnBoom()
        {
            base.OnBoom();
            if(IsHit && !JustDied) StartCoroutine(Electryfy());
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
                leftIdleAnimation=hitLeftIdleAnimation;
                rightIdleAnimation=hitRightIdleAnimation;
                var newPos = new Vector2Int(Random.Range(1, Grid.n - 1), Random.Range(1, Grid.m - 1));
                while (newPos==pos) newPos = new Vector2Int(Random.Range(1, Grid.n - 1), Random.Range(1, Grid.m - 1));
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