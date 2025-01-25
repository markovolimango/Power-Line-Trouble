using Grid;
using UnityEngine;

namespace Birds
{
    public abstract class Bird : MonoBehaviour
    {
        protected GridManager Grid;
        protected Vector2Int Pos;

        protected virtual void Start()
        {
            Grid = FindFirstObjectByType<GridManager>();
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                Die();
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}