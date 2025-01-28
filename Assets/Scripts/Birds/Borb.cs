using Grid;
using UnityEngine;

namespace Birds
{
    public class Borb : Bird
    {
        protected override void Start()
        {
            base.Start();
            Health = 2;
        }
        
        public override void GetHit()
        {
            base.GetHit();
            if (!JustDied)
            {
                IsOnHorizontal = -1;
                var newPos = new Vector2Int(Random.Range(1, Grid.n - 1), Random.Range(1, Grid.m - 1));
                while (newPos==pos) newPos = new Vector2Int(Random.Range(1, Grid.n - 1), Random.Range(1, Grid.m - 1));
                MoveBirdToPos(newPos);
            }
        }
    }
}