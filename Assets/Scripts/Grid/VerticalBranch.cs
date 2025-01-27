using UnityEngine;

namespace Grid
{
    public class VerticalBranch : Branch
    {
        protected override void ArrangeBirds()
        {
            var dir = -(EndPos - MidPos).normalized;
            var offset = (Birds.Count - 1) * spaceBetweenBirds * dir / 2;
            foreach (var bird in Birds)
            {
                bird.transform.position =
                    new Vector3(MidPos.x - offset.x, MidPos.y - offset.y, bird.transform.position.z);
                if (offset.x <= 0)
                    dir = -(MidPos - StartPos).normalized;
                offset -= spaceBetweenBirds * dir;
            }
        }
    }
}