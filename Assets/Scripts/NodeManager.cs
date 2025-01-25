using System;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public int m, n;
    public float distanceX, distanceY, skretanje;
    public GameObject nodePrefab;
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
    }

    private void Update()
    {
    }
}