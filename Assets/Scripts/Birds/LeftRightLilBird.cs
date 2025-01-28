using UnityEngine;

namespace Birds
{
    public class LeftRightLilBird : LilBird
    {
        protected override void Start()
        {
            IsOnHorizontal = 0;
            base.Start();
            JumpDir = Vector2Int.left;
        }

        public override void OnTsk()
        {
            VerticalBranches[pos.y, pos.x].DetachBird(this);
            MoveBirdToPos(pos+JumpDir);
            JumpDir *= -1;
        }
        
        public override void Die()
        {
            VerticalBranches[pos.y, pos.x].DetachBird(this);
            base.Die();
        }
        
    }
}