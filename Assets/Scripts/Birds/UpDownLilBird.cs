using UnityEngine;

namespace Birds
{
    public class UpDownLilBird : LilBird
    {
        protected override void Start()
        {
            base.Start();
            Pos.x = 3;
            Pos.y = 3;
            JumpDir = Vector2Int.up;
            Branches = Grid.HorizontalBranches;
        }
    }
}