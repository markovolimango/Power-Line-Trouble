using UnityEngine;

namespace Birds
{
    public class WatcherBird : Bird
    {
        public int waitTime;
        private int _waitTimer = 0;
        public override void Start()
        {
            IsOnHorizontal = 1;
            base.Start();
        }

        protected override void MoveBirdToPos(Vector2Int newPos)
        {
            if (pos.x != -1)
                for (var j = 0; j < Grid.n; j++)
                    Grid.NodeIsWatched[pos.y, j] = false;

            base.MoveBirdToPos(newPos);
            _waitTimer = waitTime;
        }

        public override void OnTsk()
        {
            if (_waitTimer > 0)
            {
                _waitTimer--;
                return;
            }
            else if (_waitTimer == 0)
            {
                _waitTimer--;
                for (var j = 0; j < Grid.n; j++) Grid.NodeIsWatched[pos.y, j] = true;
            }
            base.OnTsk();
        }
    }
}