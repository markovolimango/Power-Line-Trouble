using Grid;
using UnityEngine;
using System.Collections;

namespace Birds
{
    public abstract class Bird : MonoBehaviour
    {
        public float jumpHeightFactor;
        public float jumpDuration;
        public Vector2Int pos;
        protected GridManager Grid;
        protected Branch[,] HorizontalBranches;
        protected Branch[,] VerticalBranches;
        protected int IsOnHorizontal=-1; //-1 - not set, 0 - vertical, 1 - horizontal
        protected int Health = 1;
        protected bool JustDied = false;
        protected virtual void Start()
        {
            Grid = FindFirstObjectByType<GridManager>();
            HorizontalBranches = Grid.HorizontalBranches;
            VerticalBranches = Grid.VerticalBranches;
            var startingPos = pos;
            pos = new Vector2Int(-1, -1);
            MoveBirdToPos(startingPos);
        }

        protected virtual void MoveBirdToPos(Vector2Int newPos)//detachuj pticu pre nego sto pozoves ovo
        {
            var startPos = transform.position;
            var endPos = new Vector3();
            if (IsOnHorizontal == -1)
            {
                if (Random.Range(0, 2) == 0)
                {
                    IsOnHorizontal = 1;
                }
                else
                {
                    IsOnHorizontal = 0;
                }
            }
            if (IsOnHorizontal==1)
            {
                endPos = HorizontalBranches[newPos.y, newPos.x].MidPos;
            }
            else if (IsOnHorizontal==0)
            {
                endPos = VerticalBranches[newPos.y, newPos.x].MidPos;
            }

            MoveInSmoothSlurpeLine(startPos, endPos, 0.5f, 0.2f);
            if (IsOnHorizontal==1)
            {
                HorizontalBranches[newPos.y, newPos.x].AttachBird(this);
                if(pos!=new Vector2Int(-1,-1)) HorizontalBranches[pos.y, pos.x].DetachBird(this);
            }
            else if (IsOnHorizontal==0)
            {
                VerticalBranches[newPos.y, newPos.x].AttachBird(this);
                if(pos!=new Vector2Int(-1,-1)) VerticalBranches[pos.y, pos.x].DetachBird(this);
            }

            pos = newPos;
        }
        
        public void MoveInSmoothSlurpeLine(Vector2 start, Vector2 end, float height, float duration)
        {
            print(end);
            print(start);
            StartCoroutine(MoveInSmoothSlurpeLineCoroutine(start, end, (end-start).magnitude*jumpHeightFactor, jumpDuration));
        }

        private IEnumerator MoveInSmoothSlurpeLineCoroutine(Vector2 start, Vector2 end, float height, float duration)
        {
            float elapsedTime = 0f;
            Vector2 controlPoint = (start + end) / 2 + Vector2.up * height;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                t = Mathf.SmoothStep(0f, 1f, t);
                Vector2 position = CalculateBezierPoint(t, start, controlPoint, end);
                transform.position = position;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = end;
        }

        private Vector2 CalculateBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            Vector2 p = uu * p0; // (1-t)^2 * p0
            p += 2 * u * t * p1; // 2(1-t)t * p1
            p += tt * p2; // t^2 * p2
            return p;
        }
        

        public virtual void OnTsk()
        {
            
        }


        public virtual void GetHit()
        {
            Health--;
            if (IsOnHorizontal==1)
            {
                HorizontalBranches[pos.y, pos.x].DetachBird(this);
            }
            else if (IsOnHorizontal==0)
            {
                VerticalBranches[pos.y, pos.x].DetachBird(this);
            }

            if (Health <= 0)
            {
                Die();
            }
        }
        public virtual void Die()
        {
            JustDied = true;
            Destroy(gameObject);
        }
    }
}