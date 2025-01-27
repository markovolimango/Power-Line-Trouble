using System;
using System.ComponentModel;
using UnityEngine;
using Birds;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Grid
{
    public abstract class Branch : MonoBehaviour
    {
        protected LineRenderer LineRenderer;
        [NonSerialized] public Vector2 StartPos, MidPos, EndPos;
        [NonSerialized] protected List<Bird> Birds = new List<Bird>();

        public void SetEdges(Vector2 start, Vector2 end)
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
            Birds.Add(bird);
            LineRenderer.SetPosition(1, MidPos);
        }

        public virtual void DetachBird(Bird bird)
        {
            print(bird);
            Birds.Remove(bird);
            LineRenderer.SetPosition(1, MidPos);
        }

        public void KillBirds()
        {
            print("KILLING");
            foreach (var bird in Birds.ToList())
            {
                bird.Die();
            }
        }
    }
}