using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

namespace InvaderSystem
{
    public class InvaderSpawner : MonoBehaviour
    {
        [SerializeField] private AInvader[] enemyPrefab;
        [SerializeField] [Range(0.1f, 30f)] private float spawnTimer = 1f;
        [SerializeField] [Range(0f, 50f)] private int poolSize = 5;

        private GameObject[] _pool;

        private Dictionary<AInvader, GameObject[]> _poolsByEnemyType = new Dictionary<AInvader, GameObject[]>();
        private int _spawnedEnemies = 0;

        private void Awake()
        {
            PopulatePool();
        }

        private void Start()
        {
            StartCoroutine(SpawnEnemy());
        }

        public void StartSpawningEnemies(WaveDefinition waveDefinition)
        {
            
        }
        void PopulatePool()
        {
            _pool = new GameObject[poolSize];
            for (int i = 0; i < _pool.Length; i++)
            {
                _pool[i] = Instantiate(enemyPrefab[0].gameObject, transform);
                _pool[i].SetActive(false);
            }
        }

        void EnableObjectInPool()
        {
            for (int i = 0; i < _pool.Length; i++)
            {
                if (!_pool[i].activeInHierarchy)
                {
                    _pool[i].SetActive(true);
                    return;
                }
            }
        }

        IEnumerator SpawnEnemy()
        {
            while (true)
            {
                EnableObjectInPool();
                yield return new WaitForSeconds(spawnTimer);
            }
        }
    }
}