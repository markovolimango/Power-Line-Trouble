using Grid;
using UnityEngine;

namespace Birds
{
    public abstract class Bird : MonoBehaviour
    {
        public Vector2Int pos;
        protected GridManager Grid;

        protected virtual void Start()
        {
            Grid = FindFirstObjectByType<GridManager>();
        }

        public abstract void OnTsk();

        public virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}