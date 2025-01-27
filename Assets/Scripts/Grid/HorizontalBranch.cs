using System;
using Birds;

namespace Grid
{
    public class HorizontalBranch : Branch
    {
        public override void AttachBird(Bird bird)
        {
            MidPos.y -= 0.2f;
            base.AttachBird(bird);
        }

        public override void DetachBird(Bird bird)
        {
            MidPos.y = Math.Min(StartPos.y, MidPos.y + 0.2f);
            base.DetachBird(bird);
        }
    }
}