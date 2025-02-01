using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Birds
{
    public class Pelican : Bird
    {
        public int healAmount;
        private Health _carHealth;
        private Vector2Int _jumpDir;
        public ParticleSystem water;
        public ParticleSystem waterDeath;
        
        public override void Start()
        {
            IsOnHorizontal = 0;
            base.Start();
            _jumpDir = Vector2Int.left;
        }
        

        public override void GetHit()
        {
            if (JustDied) return;
            _carHealth = GameObject.FindGameObjectWithTag("Car").GetComponent<Health>();
            _carHealth.Heal(healAmount);
            water.emissionRate = 0;
            waterDeath.Play();
            base.GetHit();
        }

        public override Vector2Int GetRandomPos()
        {
            return new Vector2Int(Grid.n-1, Random.Range(0, Grid.m-1));
        }
        
        public override void OnTsk()
        {
            if (JustDied) return;
            if (pos.x == 0)
            {
                JustDied = true;
                if (IsOnHorizontal == 1)
                    HorizontalBranches[pos.y, pos.x].DetachBird(this);
                else if (IsOnHorizontal == 0) VerticalBranches[pos.y, pos.x].DetachBird(this);
                var start = transform.position;
                var end = transform.position + new Vector3(-1.5f, 0);
                StartCoroutine(MoveInSmoothSlurpeLineCoroutine(start, end, (end - start).magnitude * jumpHeightFactor, jumpDuration));
                StartCoroutine(ByeByePelican());
            }
            MoveBirdToPos(pos + _jumpDir);
        }

        private IEnumerator ByeByePelican()
        {
            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }
    }
}