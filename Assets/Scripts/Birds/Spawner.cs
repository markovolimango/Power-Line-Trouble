using Grid;
using UnityEngine;

namespace Birds
{
    public class Spawner : MonoBehaviour
    {
        public int spawnTime;
        public GameObject[] birdPrefabs;
        private DJScript _dj;
        private GridManager _grid;
        private int _spawnTimer;

        private void Start()
        {
            _dj = FindFirstObjectByType<DJScript>();
            _grid = FindObjectOfType<GridManager>();
            _spawnTimer = spawnTime;
        }

        public void OnTsk()
        {
            if (_spawnTimer > 0)
            {
                _spawnTimer--;
                return;
            }

            var spawnPos = new Vector2Int(Random.Range(1, _grid.n - 1), Random.Range(1, _grid.m - 1));
            //var spawnPos = new Vector2Int(2, 2);
            var i = Random.Range(0, birdPrefabs.Length);
            //var i = 0;
            var bird = Instantiate(birdPrefabs[i], transform);

            _dj.tsk.AddListener(bird.GetComponent<Bird>().OnTsk);
            _dj.boom.AddListener(bird.GetComponent<Bird>().OnBoom);
            bird.GetComponent<Bird>().pos = spawnPos;

            _spawnTimer = spawnTime;
        }
    }
}