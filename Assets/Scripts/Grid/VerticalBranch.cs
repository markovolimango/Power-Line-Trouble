using Birds;
using UnityEngine;

namespace Grid
{
    public class VerticalBranch : Branch
    {
        private Vector2 _dir, _perpendicular;

        private void Start()
        {
            _dir = (StartPos - EndPos).normalized;
            _perpendicular = new Vector2(-_dir.y, _dir.x);
        }

        public override void AttachBird(Bird bird)
        {
            MidPos -= _perpendicular * 0.2f;
            base.AttachBird(bird);
        }

        public override void DetachBird(Bird bird)
        {
            if (MidPos == (StartPos + EndPos) / 2) return;
            MidPos += _perpendicular * 0.2f;
            base.DetachBird(bird);
        }
    }
}