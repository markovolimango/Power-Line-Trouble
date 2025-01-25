using System;
using UnityEngine;

namespace Grid
{
    public abstract class Branch : MonoBehaviour
    {
        protected LineRenderer _lineRenderer;
        [NonSerialized] public Vector2 StartPos, MidPos, EndPos;

        protected void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void SetEdges(Vector2 start, Vector2 end)
        {
            _lineRenderer = GetComponent<LineRenderer>();
            StartPos = start;
            _lineRenderer.SetPosition(0, start);
            MidPos = (start + end) / 2;
            _lineRenderer.SetPosition(1, (start + end) / 2);
            EndPos = end;
            _lineRenderer.SetPosition(2, end);
        }

        public void AttachBird()
        {
            MidPos.y -= 0.2f;
            _lineRenderer.SetPosition(1, MidPos);
        }

        public void DetachBird()
        {
            MidPos.y = Math.Min(StartPos.y, MidPos.y + 0.2f);
            _lineRenderer.SetPosition(1, MidPos);
        }
    }
}