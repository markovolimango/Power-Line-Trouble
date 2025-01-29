using UnityEngine;

namespace Birds
{
    public class Leg : Bird
    {
        private Vector2Int _jumpDir;
        private LongBird _longBird;

        protected override void Start()
        {
            _longBird = transform.parent.GetComponent<LongBird>();
            base.Start();
        }

        public override void GetHit()
        {
            if (!IsInBounds(pos + _jumpDir)) _longBird.Die();
            MoveBirdToPos(pos + _jumpDir);
        }
    }
}