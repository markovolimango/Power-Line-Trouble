using UnityEngine;

namespace Birds
{
    public abstract class Brd : Bird
    {
        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                Die();
        }

        public abstract void Jump();

        protected void Die()
        {
            Destroy(gameObject);
        }
    }
}