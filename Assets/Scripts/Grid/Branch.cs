using System;
using UnityEngine;

namespace Grid
{
    public abstract class Branch : MonoBehaviour
    {
        protected LineRenderer LineRenderer;
        [NonSerialized] public Vector2 StartPos, MidPos, EndPos;

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

        public virtual void AttachBird()
        {
            LineRenderer.SetPosition(1, MidPos);
        }

        public virtual void DetachBird()
        {
            LineRenderer.SetPosition(1, MidPos);
        }
    }
}