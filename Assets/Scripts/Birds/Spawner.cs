using Grid;
using UnityEngine;
using Random = UnityEngine.Random;

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
            //print(spawnPos);
            var i = Random.Range(0, birdPrefabs.Length);
            //print(i);
            var bird = Instantiate(birdPrefabs[i], transform);
            bird.GetComponent<Bird>().pos = spawnPos;

            _dj.tsk.AddListener(bird.GetComponent<Bird>().OnTsk);

            _spawnTimer = spawnTime;
        }
    }
}