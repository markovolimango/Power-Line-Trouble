using UnityEngine;

namespace Birds
{
    public class LeftRightBrd : Brd
    {
        protected override void Start()
        {
            base.Start();
            Pos.x = 3;
            Pos.y = 3;
            JumpDir = Vector2Int.left;
            Branches = Grid.VerticalBranches;
        }
    }
}