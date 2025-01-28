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
            print(JustDied);
            if (!JustDied)
            {
                IsOnHorizontal = -1;
                MoveBirdToPos(new Vector2Int(Random.Range(1, Grid.n - 1), Random.Range(1, Grid.m - 1)));
            }
        }
    }
}