using UnityEngine;

namespace Birds
{
    public class WatcherBird : Bird
    {
        public int waitTime;
        private LineRenderer _laser;
        private int _waitTimer;

        public override void Start()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            _laser = transform.GetChild(0).GetComponent<LineRenderer>();
            pos.y = 0;
            print(pos);
            MarkNodesAs(true);
            IsOnHorizontal = 0;
            base.Start();
        }

        protected override void MoveBirdToPos(Vector2Int newPos)
        {
            if (JustDied)
                return;
            if (pos.x != -1)
                MarkNodesAs(false);

            base.MoveBirdToPos(newPos);

            MarkNodesAs(true);
            _laser.transform.position =
                new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            _laser.enabled = true;
            _laser.SetPosition(0, Grid.NodePositions[1, pos.x]);
            _laser.SetPosition(1, Grid.NodePositions[Grid.m - 1, pos.x]);
            Animator.Play(leftIdleAnimation.name);

            _waitTimer = waitTime;
        }

        public override void OnScare()
        {
            particles.Play();
            var newPos = pos;
            while (newPos == pos)
            {
                newPos = GetRandomPos();
                newPos.y = 0;
            }

            MoveBirdToPos(newPos);
        }

        public override void OnTsk()
        {
            if (JustDied) return;
            if (_waitTimer > 0)
            {
                _waitTimer--;
                return;
            }

            if (_waitTimer == 0) _waitTimer--;
            base.OnTsk();
        }

        public override void GetHit()
        {
            MarkNodesAs(false);
            _laser.enabled = false;
            base.GetHit();
        }

        private void MarkNodesAs(bool isWatched)
        {
            for (var i = 1; i < Grid.m; i++) Grid.NodeIsWatched[i, pos.x] = isWatched;
        }
    }
}