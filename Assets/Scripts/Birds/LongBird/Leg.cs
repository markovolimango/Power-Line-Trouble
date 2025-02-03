using System;
using Grid;
using UnityEngine;

namespace Birds
{
    public class Leg : Bird
    {
        private Branch[,] _branches;
        private LongBird _longBird;
        [NonSerialized] public Vector2Int JumpDir;
        [NonSerialized] public Vector2Int TargetPos;
        [NonSerialized] public bool _isLeft;

        public override void Start()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 1);
            _longBird = transform.parent.GetComponent<LongBird>();
            base.Start();
            _branches = IsOnHorizontal == 1 ? HorizontalBranches : VerticalBranches;
        }

        public override void OnScare()
        {
        }

        public override void GetHit()
        {
            if (_longBird.JustDied) return;
            _longBird.ShitTimer = _longBird.shitTime;
            _longBird.birdHit.Invoke();
            TargetPos = pos + 2 * JumpDir;
            if (TargetPos.x < 0 || TargetPos.x >= Grid.n || TargetPos.y < 0 || TargetPos.y >= Grid.m)
            {
                TargetPos = pos + JumpDir;
                if (TargetPos.x < 0 || TargetPos.x >= Grid.n || TargetPos.y < 0 || TargetPos.y >= Grid.m)
                {
                    _branches[pos.y, pos.x].DetachBird(this);
                    _longBird.Die();
                    return;
                }
                _branches[pos.y, pos.x].DetachBird(this);
                _longBird.SwapLegs(_isLeft,true);
                return;
            }

            _branches[pos.y, pos.x].DetachBird(this);
            pos = TargetPos;
            _branches[pos.y, pos.x].AttachBird(this);
            _longBird.SwapLegs(_isLeft,false);
        }

        public override void Die()
        {
            Destroy(gameObject);
        }
    }
}