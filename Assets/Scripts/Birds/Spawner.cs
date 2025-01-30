using Grid;
using UnityEngine;

namespace Birds
{
    public class Spawner : MonoBehaviour
    {
        public int spawnTime;
        public GameObject[] birdPrefabs;
        private ComboMeter _combo;
        private DJScript _dj;
        private GridManager _grid;
        private int _spawnTimer;

        private void Start()
        {
            _dj = FindFirstObjectByType<DJScript>();
            _grid = FindObjectOfType<GridManager>();
            _spawnTimer = spawnTime;
            _combo = FindObjectOfType<ComboMeter>();
        }

        public void OnTsk()
        {
            if (_spawnTimer > 0)
            {
                _spawnTimer--;
                return;
            }

            var spawnPos = new Vector2Int(Random.Range(1, _grid.n - 1), Random.Range(1, _grid.m - 1));
            //var spawnPos = new Vector2Int(3, 3);
            //var i = Random.Range(0, birdPrefabs.Length);
            var i = 5;
            var birdObject = Instantiate(birdPrefabs[i], transform);
            var bird = birdObject.GetComponent<Bird>();

            _dj.tsk.AddListener(bird.OnTsk);
            _dj.boom.AddListener(bird.OnBoom);
            bird.birdHit.AddListener(_combo.OnBirdHit);
            bird.pos = spawnPos;

            _spawnTimer = spawnTime;
        }
    }
}