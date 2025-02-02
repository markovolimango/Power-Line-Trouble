using Grid;
using UnityEngine;

namespace Birds
{
    public class StandingLilBird : LilBird
    {
        public override void Start()
        {
            base.Start();
            if (Random.Range(0, 2) == 0) Animator.Play(leftIdleAnimation.name);
            else Animator.Play(rightIdleAnimation.name);
        }
    }
}