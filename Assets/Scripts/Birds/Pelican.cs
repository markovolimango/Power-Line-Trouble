using System.Collections;
using DefaultNamespace.GameManager;
using UnityEngine;

namespace Birds
{
    public class Pelican : Bird
    {
        public int healAmount;
        public ParticleSystem water;
        public ParticleSystem waterDeath;
        private Health _carHealth;
        private Vector2Int _jumpDir;
        private bool goingLeft;
        private bool isSpawned = false;

        public override void Start()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -2);
            IsOnHorizontal = 0;
            base.Start();
            if (goingLeft) Animator.Play(leftIdleAnimation.name);
            else Animator.Play(rightIdleAnimation.name);
            isSpawned = true;
            water.Play();
        }


        public override void GetHit()
        {
            if (JustDied) return;
            _carHealth = GameObject.FindGameObjectWithTag("Car").GetComponent<Health>();
            _carHealth.Heal(healAmount);
            var emission = water.emission;
            emission.rateOverTime = 0;
            waterDeath.Play();
            base.GetHit();
        }

        public override Vector2Int GetRandomPos()
        {
            if (!isSpawned)
            {
                if (Random.Range(0, 2) == 0)
                {
                    print("left");
                    _jumpDir = Vector2Int.left;
                    goingLeft = true;
                    return new Vector2Int(Grid.n - 1, Random.Range(0, Grid.m - 1));
                }

                print("right");
                _jumpDir = Vector2Int.right;
                goingLeft = false;
                return new Vector2Int(0, Random.Range(0, Grid.m - 1));
            }
            else
            {
                if (goingLeft)
                {
                    print("left");
                    _jumpDir = Vector2Int.left;
                    goingLeft = true;
                    return new Vector2Int(Grid.n - 1, Random.Range(0, Grid.m - 1));
                }

                print("right");
                _jumpDir = Vector2Int.right;
                goingLeft = false;
                return new Vector2Int(0, Random.Range(0, Grid.m - 1));
            }
        }

        public override void OnTsk()
        {
            if (JustDied) return;
            if ((pos.x == 0 && goingLeft) || (pos.x == Grid.n - 1 && !goingLeft))
            {
                JustDied = true;
                if (IsOnHorizontal == 1)
                    HorizontalBranches[pos.y, pos.x].DetachBird(this);
                else if (IsOnHorizontal == 0) VerticalBranches[pos.y, pos.x].DetachBird(this);
                var start = transform.position;
                Vector3 end;
                if (goingLeft) end = transform.position + new Vector3(-1.5f, 0);
                else end = transform.position + new Vector3(1.5f, 0);
                StartCoroutine(MoveInSmoothSlurpeLineCoroutine(start, end, (end - start).magnitude * jumpHeightFactor,
                    jumpDuration));
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