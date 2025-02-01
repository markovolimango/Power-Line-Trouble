using UnityEngine;

namespace Birds
{
    public class LeftRightLilBird : LilBird
    {
        public override void Start()
        {
            IsOnHorizontal = 0;
            base.Start();
            JumpDir = Vector2Int.left;
        }

        public override Vector2Int GetRandomPos()
        {
            var random_pos= new Vector2Int(Random.Range(1, Grid.n-1), Random.Range(0, Grid.m-1));
            while(random_pos==pos || random_pos==new Vector2Int(Grid.n-1,Grid.m-1)) random_pos= new Vector2Int(Random.Range(1, Grid.n-1), Random.Range(0, Grid.m-1));
            return random_pos;
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