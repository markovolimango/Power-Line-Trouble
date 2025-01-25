using UnityEngine;

public abstract class BranchPhysics : MonoBehaviour
{
    protected LineRenderer _lineRenderer;

    protected void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetEdges(Vector2 start, Vector2 end)
    {
        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, (start + end) / 2);
        _lineRenderer.SetPosition(2, end);
    }
}