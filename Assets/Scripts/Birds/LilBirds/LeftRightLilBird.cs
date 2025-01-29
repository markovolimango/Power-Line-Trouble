using UnityEngine;

namespace Birds
{
    public class LeftRightLilBird : LilBird
    {
        protected override void Start()
        {
            IsOnHorizontal = 0;
            base.Start();
            JumpDir = Vector2Int.left;
        }

        public override void OnTsk()
        {
            if (JustDied) return;
            MoveBirdToPos(pos + JumpDir);
            JumpDir *= -1;
            if(JumpDir==Vector2Int.left) Animator.Play(leftIdleAnimation.name);
            else Animator.Play(rightIdleAnimation.name);
        }
    }
}