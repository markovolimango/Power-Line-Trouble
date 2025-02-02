using System;
using UnityEngine;

namespace Grid
{
    public class GridManager : MonoBehaviour
    {
        public int m, n;
        public float distanceX, distanceY, skretanje;
        public GameObject nodePrefab;
        public GameObject horizontalBranchPrefab, verticalBranchPrefab;
        private AudioSource _scareAudioSource;
        [NonSerialized] public Branch[,] HorizontalBranches;
        [NonSerialized] public bool[,] NodeIsWatched;
        [NonSerialized] public Vector2[,] NodePositions;
        [NonSerialized] public Branch[,] VerticalBranches;

        private void Start()
        {
            NodePositions = new Vector2[m, n];
            NodeIsWatched = new bool[m, n];
            _scareAudioSource = GetComponent<AudioSource>();
            var pos = transform.position;
            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    Instantiate(nodePrefab, pos, transform.rotation, transform);
                    NodePositions[i, j] = pos;
                    pos.x += distanceX;
                }

                pos.x -= n * distanceX;
                pos.x -= skretanje;
                pos.y -= distanceY;
            }

            HorizontalBranches = new Branch[m, n - 1];
            for (var i = 0; i < m; i++)
            for (var j = 0; j < n - 1; j++)
            {
                var branch = Instantiate(horizontalBranchPrefab, NodePositions[i, j], transform.rotation, transform);
                branch.transform.position = new Vector3(branch.transform.position.x, branch.transform.position.y, 5);
                HorizontalBranches[i, j] = branch.GetComponent<HorizontalBranch>();
                HorizontalBranches[i, j].SetEdges(NodePositions[i, j], NodePositions[i, j + 1]);
            }

            VerticalBranches = new Branch[m - 1, n];
            for (var j = 0; j < n; j++)
            for (var i = 0; i < m - 1; i++)
            {
                var branch = Instantiate(verticalBranchPrefab, NodePositions[i, j], transform.rotation, transform);
                branch.transform.position = new Vector3(branch.transform.position.x, branch.transform.position.y, 5);
                VerticalBranches[i, j] = branch.GetComponent<VerticalBranch>();
                VerticalBranches[i, j].SetEdges(NodePositions[i, j], NodePositions[i + 1, j]);
            }
        }

        public void ScareBirds()
        {
            _scareAudioSource.Play();
            foreach (var br in HorizontalBranches) br.ScareBirds();
            foreach (var br in VerticalBranches) br.ScareBirds();
        }
    }
}