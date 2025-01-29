using Grid;
using UnityEngine;

namespace Birds
{
    public abstract class LilBird : Bird
    {
        protected Branch[,] Branches;

        protected Vector2Int JumpDir;

        public override void OnTsk()
        {
            Branches[pos.y, pos.x].DetachBird(this);
            pos += JumpDir;
            Branches[pos.y, pos.x].AttachBird(this);
            transform.position = new Vector3(transform.position.x, transform.position.y, pos.y - 2.5f);
            JumpDir *= -1;
        }

        public override void Die()
        {
            Branches[pos.y, pos.x].DetachBird(this);
            base.Die();
        }
    }
}