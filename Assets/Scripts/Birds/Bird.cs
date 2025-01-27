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
        protected int IsOnHorizontal=-1;
        protected virtual void Start()
        {
            Grid = FindFirstObjectByType<GridManager>();
            HorizontalBranches = Grid.HorizontalBranches;
            VerticalBranches = Grid.VerticalBranches;

            if (IsOnHorizontal == -1)
            {
                if (Random.Range(0, 2) == 0)
                {
                    HorizontalBranches[pos.y, pos.x].AttachBird(this);
                    IsOnHorizontal = 1;
                }
                else
                {
                    VerticalBranches[pos.y, pos.x].AttachBird(this);
                    IsOnHorizontal = 0;
                }
            }
        }

        public abstract void OnTsk();

        public virtual void Die()
        {
            if (IsOnHorizontal==1)
            {
                HorizontalBranches[pos.y, pos.x].DetachBird(this);
            }
            else
            {
                VerticalBranches[pos.y, pos.x].DetachBird(this);
            }
            Destroy(gameObject);
        }
    }
}