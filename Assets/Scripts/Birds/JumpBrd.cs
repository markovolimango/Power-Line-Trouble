using Grid;
using UnityEngine;

namespace Birds
{
    public class JumpBrd : Brd
    {
        public float jumpTime;
        private GridManager _gridManager;
        private int _jumpDir;
        private int _posX, _posY;

        private void Start()
        {
            _gridManager = FindFirstObjectByType<GridManager>();
            _posX = 3;
            _posY = 3;
            _jumpDir = 1;
        }

        public override void Jump()
        {
            _gridManager.HorizontalBranches[_posY, _posX].DetachBird();
            _posY += _jumpDir;
            _gridManager.HorizontalBranches[_posY, _posX].AttachBird();
            transform.position = _gridManager.HorizontalBranches[_posY, _posX].MidPos + Vector2.up * 0.4f;
            _jumpDir *= -1;
        }
    }
}