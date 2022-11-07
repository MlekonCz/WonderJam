using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace InvaderSystem
{
    public class InvaderSpawner : MonoBehaviour
    {
        [FormerlySerializedAs("_EnemyPrefab")] [SerializeField] private GameObject enemyPrefab;
        [FormerlySerializedAs("_SpawnTimer")] [SerializeField] [Range(0.1f, 30f)] private float spawnTimer = 1f;
        [FormerlySerializedAs("_PoolSize")] [SerializeField] [Range(0f, 50f)] private int poolSize = 5;

        private GameObject[] _pool;

        private int _spawnedEnemies = 0;

        private void Awake()
        {
            PopulatePool();
        }

        private void Start()
        {
            StartCoroutine(SpawnEnemy());
        }

        void PopulatePool()
        {
            _pool = new GameObject[poolSize];
            for (int i = 0; i < _pool.Length; i++)
            {
                _pool[i] = Instantiate(enemyPrefab, transform);
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