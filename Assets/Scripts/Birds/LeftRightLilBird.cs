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
            VerticalBranches[pos.y, pos.x].AttachBird(this);
        }

        public override void OnTsk()
        {
            VerticalBranches[pos.y, pos.x].DetachBird(this);
            pos += JumpDir;
            VerticalBranches[pos.y, pos.x].AttachBird(this);
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
            JumpDir *= -1;
        }
        
        public override void Die()
        {
            VerticalBranches[pos.y, pos.x].DetachBird(this);
            base.Die();
        }
        
    }
}