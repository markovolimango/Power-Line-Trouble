using UnityEngine;

namespace Birds
{
    public class FullLeftRightLilBird : LilBird
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
            if (pos.x == 0 || pos.x == Grid.n - 1) JumpDir *= -1;
            if(JumpDir==Vector2Int.left) Animator.Play(leftIdleAnimation.name);
            else Animator.Play(rightIdleAnimation.name);
        }
    }
}