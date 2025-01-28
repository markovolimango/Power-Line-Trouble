using Grid;
using UnityEngine;

namespace Birds
{
    public abstract class Bird : MonoBehaviour
    {
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
            MoveBirdToPos(pos);
        }

        protected virtual void MoveBirdToPos(Vector2Int newPos)//detachuj pticu pre nego sto pozoves ovo
        {
            if (IsOnHorizontal == -1)
            {
                if (Random.Range(0, 2) == 0)
                {
                    HorizontalBranches[newPos.y, newPos.x].AttachBird(this);
                    IsOnHorizontal = 1;
                }
                else
                {
                    VerticalBranches[newPos.y, newPos.x].AttachBird(this);
                    IsOnHorizontal = 0;
                }
            }
            else if (IsOnHorizontal==1)
            {
                HorizontalBranches[newPos.y, newPos.x].AttachBird(this);
            }
            else if (IsOnHorizontal==0)
            {
                VerticalBranches[newPos.y, newPos.x].AttachBird(this);
            }

            pos = newPos;
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