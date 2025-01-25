using Grid;
using UnityEngine;

namespace Birds
{
    public abstract class Brd : Bird
    {
        protected Branch[,] Branches;

        protected Vector2Int JumpDir;

        public void OnTsk()
        {
            print(Pos);
            print(Branches[Pos.x, Pos.y].name);
            print(1);
            Branches[Pos.y, Pos.x].DetachBird();
            Pos += JumpDir;
            print(2);
            Branches[Pos.y, Pos.x].AttachBird();
            print(3);
            transform.position = Branches[Pos.y, Pos.x].MidPos + Vector2.up * 0.3f;
            print(4);
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
            JumpDir *= -1;
            print(5);
        }

        protected override void Die()
        {
            Branches[Pos.y, Pos.x].DetachBird();
            base.Die();
        }
    }
}