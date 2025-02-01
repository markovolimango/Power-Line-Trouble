using UnityEngine;

namespace Birds
{
    public class WatcherBird : Bird
    {
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
            for (var j = 0; j < Grid.n; j++) Grid.NodeIsWatched[pos.y, j] = true;
        }
    }
}