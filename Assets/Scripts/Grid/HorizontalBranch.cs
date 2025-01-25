using System;

namespace Grid
{
    public class HorizontalBranch : Branch
    {
        public override void AttachBird()
        {
            MidPos.y -= 0.2f;
            base.AttachBird();
        }

        public override void DetachBird()
        {
            MidPos.y = Math.Min(StartPos.y, MidPos.y + 0.2f);
            base.AttachBird();
        }
    }
}