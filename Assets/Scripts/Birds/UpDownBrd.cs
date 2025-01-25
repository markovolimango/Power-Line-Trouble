using UnityEngine;

namespace Birds
{
    public class UpDownBrd : Brd
    {
        protected override void Start()
        {
            base.Start();
            Pos.x = 3;
            Pos.y = 3;
            JumpDir = Vector2Int.up;
            Branches = Grid.HorizontalBranches;
            for (var i = 0; i < Branches.GetLength(0); i++)
            for (var j = 0; j < Branches.GetLength(1); j++)
                print(Branches[i, j].name);
        }
    }
}