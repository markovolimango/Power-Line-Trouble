using UnityEngine;

namespace Birds
{
    public class LongBird : Bird
    {
        private Leg _leftLeg, _rightLeg;

        protected override void Start()
        {
            _leftLeg = transform.GetChild(0).GetComponent<Leg>();
            _leftLeg.pos = pos;
            _rightLeg = transform.GetChild(1).GetComponent<Leg>();
            _rightLeg.pos = pos + Vector2Int.right;
        }
    }
}