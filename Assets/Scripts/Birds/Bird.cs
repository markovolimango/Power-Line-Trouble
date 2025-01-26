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

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                Die();
        }

        public abstract void OnTsk();

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}