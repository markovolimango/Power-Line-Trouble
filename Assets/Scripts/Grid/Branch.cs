using System;
using System.Collections.Generic;
using System.Linq;
using Birds;
using UnityEngine;

namespace Grid
{
    public abstract class Branch : MonoBehaviour
    {
        public float spaceBetweenBirds;
        [NonSerialized] protected List<Bird> Birds = new();
        protected LineRenderer LineRenderer;
        [NonSerialized] public Vector2 StartPos, MidPos, EndPos;

        public virtual void SetEdges(Vector2 start, Vector2 end)
        {
            LineRenderer = GetComponent<LineRenderer>();
            StartPos = start;
            LineRenderer.SetPosition(0, start);
            MidPos = (start + end) / 2;
            LineRenderer.SetPosition(1, (start + end) / 2);
            EndPos = end;
            LineRenderer.SetPosition(2, end);
        }

        public virtual void AttachBird(Bird bird)
        {
            MidPos.y -= 0.2f;
            Birds.Add(bird);
            LineRenderer.SetPosition(1, MidPos);
            ArrangeBirds();
        }

        public void DetachBird(Bird bird)
        {
            if (MidPos != (StartPos + EndPos) / 2)
                MidPos.y += 0.2f;
            print(bird);
            Birds.Remove(bird);
            LineRenderer.SetPosition(1, MidPos);
            ArrangeBirds();
        }

        public void KillBirds()
        {
            print("KILLING");
            foreach (var bird in Birds.ToList()) bird.Die();
        }

        protected abstract void ArrangeBirds();
    }
}