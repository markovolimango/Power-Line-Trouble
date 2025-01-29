using UnityEngine;

namespace Birds
{
    public class UpDownLilBird : LilBird
    {
        protected override void Start()
        {
            IsOnHorizontal = 1;
            base.Start();
            JumpDir = Vector2Int.up;
        }

        public override void OnTsk()
        {
            if (JustDied) return;
            MoveBirdToPos(pos + JumpDir);
            JumpDir *= -1;
            if(JumpDir==Vector2Int.up) Animator.Play(leftIdleAnimation.name);
            else Animator.Play(rightIdleAnimation.name);
        }
    }
}