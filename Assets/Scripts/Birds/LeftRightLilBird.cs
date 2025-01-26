using UnityEngine;

namespace Birds
{
    public class LeftRightLilBird : LilBird
    {
        protected override void Start()
        {
            base.Start();
            JumpDir = Vector2Int.left;
            Branches = Grid.VerticalBranches;
        }
    }
}