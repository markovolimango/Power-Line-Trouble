using UnityEngine;

namespace Birds
{
    public class UpDownLilBird : LilBird
    {
        protected override void Start()
        {
            IsOnHorizontal = 1;
            base.Start();
            JumpDir = Vector2Int.up;
        }

        public override void OnTsk()
        {
            MoveBirdToPos(pos + JumpDir);
            JumpDir *= -1;
        }
    }
}