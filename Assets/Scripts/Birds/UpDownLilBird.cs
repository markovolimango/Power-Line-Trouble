using UnityEngine;

namespace Birds
{
    public class UpDownLilBird : LilBird
    {
        protected override void Start()
        {
            base.Start();
            JumpDir = Vector2Int.up;
            Branches = Grid.HorizontalBranches;
        }
    }
}