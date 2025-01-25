using Grid;
using UnityEngine;

namespace Birds
{
    public abstract class LilBird : Bird
    {
        protected Branch[,] Branches;

        protected Vector2Int JumpDir;

        public void OnTsk()
        {
            print(Branches[Pos.x, Pos.y].name);
            Branches[Pos.y, Pos.x].DetachBird();
            Pos += JumpDir;
            Branches[Pos.y, Pos.x].AttachBird();
            transform.position = Branches[Pos.y, Pos.x].MidPos + Vector2.up * 0.3f;
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
            JumpDir *= -1;
        }

        protected override void Die()
        {
            Branches[Pos.y, Pos.x].DetachBird();
            base.Die();
        }
    }
}