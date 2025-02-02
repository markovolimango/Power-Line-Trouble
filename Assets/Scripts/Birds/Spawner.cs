using DefaultNamespace.GameManager;
using System.Collections.Generic;
using DefaultNamespace.GameManager;
using Grid;
using UnityEngine;
using UnityEngine.Serialization;

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
        private Score _scoreScript;
        private int _pelicanSpawnTime;
        private int _pelicanSpawnTimer;
        public List<int> scoreLvl;
        public List<int> spawnTimeLvl;
        public List<int> birdsLvl;
        public List<int> pelicanTimeLvl;
        private int _currentLvl;
        private int _birdsIndex;
        


        private void Start()
        {
            _currentLvl = 0;
            spawnTime = spawnTimeLvl[_currentLvl];
            _pelicanSpawnTime = pelicanTimeLvl[_currentLvl];
            _birdsIndex = birdsLvl[_currentLvl];
            _pelicanSpawnTimer = _pelicanSpawnTime;
            _dj = FindFirstObjectByType<DJScript>();
            _grid = FindObjectOfType<GridManager>();
            _spawnTimer = 2;
            _combo = FindObjectOfType<ComboMeter>();
            _scoreScript = FindFirstObjectByType<Score>();
            scoreLvl[scoreLvl.Count - 1] = int.MaxValue;
        }

        public void OnTsk()
        {
            int i=0;
            if(_scoreScript._score >= scoreLvl[_currentLvl])
            {
                _currentLvl++;
                spawnTime = spawnTimeLvl[_currentLvl];
                _pelicanSpawnTime = pelicanTimeLvl[_currentLvl];
                _birdsIndex = birdsLvl[_currentLvl];
                
                _pelicanSpawnTimer=Mathf.Min(_pelicanSpawnTime,_pelicanSpawnTimer);
                
                i=birdsLvl[_currentLvl]-1;
                _spawnTimer = 0;
            }

            if (i == 0)
            {
                if (_spawnTimer > 0)
                {
                    _spawnTimer--;
                    return;
                }

                if (_pelicanSpawnTimer > 0)
                {
                    _pelicanSpawnTimer--;
                    i = Random.Range(0, _birdsIndex);
                }
                else
                {
                    i = 9;
                    _pelicanSpawnTimer = Random.Range(_pelicanSpawnTime, _pelicanSpawnTime + 2);
                }
            }
            var birdObject = Instantiate(birdPrefabs[i], transform);
            var bird = birdObject.GetComponent<Bird>();
            bird.Grid = _grid;
            var spawnPos = bird.GetRandomPos();
            _dj.tsk.AddListener(bird.OnTsk);
            _dj.boom.AddListener(bird.OnBoom);
            bird.birdHit.AddListener(_combo.OnBirdHit);
            bird.pos = spawnPos;

            _spawnTimer = spawnTime;
        }
    }
}