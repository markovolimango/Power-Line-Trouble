using UnityEngine;

namespace Birds
{
    public class UpDownLilBird : LilBird
    {
        protected override void Start()
        {
            IsOnHorizontal= 1;
            base.Start();
            JumpDir = Vector2Int.up;
        }
        
        public override void OnTsk()
        {
            HorizontalBranches[pos.y, pos.x].DetachBird(this);
            pos += JumpDir;
            HorizontalBranches[pos.y, pos.x].AttachBird(this);
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
            JumpDir *= -1;
        }
        
        public override void Die()
        {
            HorizontalBranches[pos.y, pos.x].DetachBird(this);
            base.Die();
        }
    }
}