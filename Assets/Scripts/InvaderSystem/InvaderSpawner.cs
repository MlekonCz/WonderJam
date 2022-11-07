using System.Collections;
using UnityEngine;

namespace InvaderSystem
{
    public class InvaderSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] [Range(0.1f,30f)] private float spawnTimer = 1f;
        [SerializeField] [Range(0f,50f)]private int poolSize = 5;

        private GameObject[] pool;

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
            pool = new GameObject[poolSize];
            for (int i = 0; i < pool.Length; i++)
            {
                pool[i] = Instantiate(enemyPrefab, transform);
                pool[i].SetActive(false);
            }
        }

        void EnableObjectInPool()
        {
            for (int i = 0; i <pool.Length; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    pool[i].SetActive(true);
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