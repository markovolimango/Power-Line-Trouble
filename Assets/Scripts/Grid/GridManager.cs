using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int m, n;
    public float distanceX, distanceY, skretanje;
    public GameObject nodePrefab;
    public GameObject horizontalBranchPrefab, verticalBranchPrefab;
    [NonSerialized] public Vector2[,] NodePositions;


    private void Start()
    {
        NodePositions = new Vector2[m, n];
        var pos = transform.position;
        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                Instantiate(nodePrefab, pos, transform.rotation);
                NodePositions[i, j] = pos;
                pos.x += distanceX;
            }

            pos.x -= n * distanceX;
            pos.x -= skretanje;
            pos.y -= distanceY;
        }

        for (var i = 0; i < m; i++)
        for (var j = 0; j < n - 1; j++)
        {
            var branch = Instantiate(horizontalBranchPrefab, NodePositions[i, j], transform.rotation);
            var branchLineRenderer = branch.GetComponent<LineRenderer>();
            Vector2 start = NodePositions[i, j], end = NodePositions[i, j + 1];
            branchLineRenderer.SetPosition(0, start);
            branchLineRenderer.SetPosition(1, new Vector2((end.x + start.x) / 2, (end.y + start.y) / 2 - 0.1f));
            branchLineRenderer.SetPosition(2, end);
        }

        for (var j = 0; j < n; j++)
        for (var i = 0; i < m - 1; i++)
        {
            var branch = Instantiate(verticalBranchPrefab, NodePositions[i, j], transform.rotation);
            var branchLineRenderer = branch.GetComponent<LineRenderer>();
            Vector2 start = NodePositions[i, j], end = NodePositions[i + 1, j];
            branchLineRenderer.SetPosition(0, start);
            branchLineRenderer.SetPosition(1, new Vector2((start.x + end.x) / 2 + 0.1f, (start.y + end.y) / 2));
            branchLineRenderer.SetPosition(2, end);
        }
    }
}