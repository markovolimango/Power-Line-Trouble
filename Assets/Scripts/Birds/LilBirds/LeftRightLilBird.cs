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
            MoveBirdToPos(pos + JumpDir);
            JumpDir *= -1;
        }
    }
}