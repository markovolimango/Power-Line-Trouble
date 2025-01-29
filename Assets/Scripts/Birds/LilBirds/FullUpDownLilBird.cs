using UnityEngine;

namespace Birds
{
    public class FullUpDownLilBird : LilBird
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
            if (pos.y == 0 || pos.y == Grid.m - 1) JumpDir *= -1;
        }
    }
}