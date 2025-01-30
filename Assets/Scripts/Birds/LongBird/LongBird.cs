using Grid;
using Shaders;
using UnityEngine;

namespace Birds
{
    public class LongBird : Bird
    {
        private Leg _leftLeg, _rightLeg;

        protected override void Start()
        {
            IsOnHorizontal = Random.Range(0, 2);
            Grid = FindFirstObjectByType<GridManager>();
            _pulseShaderController = GetComponent<PulseShaderController>();

            _leftLeg = transform.GetChild(0).GetComponent<Leg>();
            _leftLeg.IsOnHorizontal = IsOnHorizontal;
            _leftLeg.pos = pos;
            if (IsOnHorizontal == 0)
                _leftLeg.JumpDir = Vector2Int.right;
            else _leftLeg.JumpDir = Vector2Int.up;
            _rightLeg = transform.GetChild(1).GetComponent<Leg>();
            _rightLeg.IsOnHorizontal = IsOnHorizontal;
            if (IsOnHorizontal == 0)
            {
                _rightLeg.pos = pos + Vector2Int.right;
                _rightLeg.JumpDir = Vector2Int.left;
            }
            else
            {
                _rightLeg.pos = pos + Vector2Int.up;
                _rightLeg.JumpDir = Vector2Int.down;
            }
        }

        private void FixedUpdate()
        {
            if (JustDied) return;
            transform.position = (_leftLeg.transform.position + _rightLeg.transform.position) / 2;
        }

        public void SwapLegs()
        {
            (_leftLeg, _rightLeg) = (_rightLeg, _leftLeg);
            if (IsOnHorizontal == 0)
                _leftLeg.JumpDir = Vector2Int.right;
            else _leftLeg.JumpDir = Vector2Int.up;
            if (IsOnHorizontal == 0)
                _rightLeg.JumpDir = Vector2Int.left;
            else _rightLeg.JumpDir = Vector2Int.down;
        }

        public override void Die()
        {
            JustDied = true;
            _leftLeg.Die();
            _rightLeg.Die();
            Destroy(gameObject);
        }
    }
}