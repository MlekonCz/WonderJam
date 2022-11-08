using DefaultNamespace;
using UnityEngine;

namespace Core
{
    public class GlobalManager : MonoBehaviour
    {

        public static GlobalManager Instance;

        [SerializeField] private GameManager gameManagerPrefab;
        [SerializeField] private SceneLoader _sceneLoaderPrefab;


        private GameManager _gameManagerInstance;
        private SceneLoader _sceneLoaderInstance;

        public GameManager GameManager => _gameManagerInstance;
        public SceneLoader SceneLoader => _sceneLoaderInstance;

        private static bool _hasSpawned = false;

        private void Awake()
        {
            if (Instance == null)
            {
                if (_hasSpawned){return;}
                SpawnPersistentObject();
                _hasSpawned = true;
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            
            else
            {
                Destroy(gameObject);
            }
           
        }

        private void SpawnPersistentObject()
        {
            _gameManagerInstance = Instantiate(gameManagerPrefab,transform);
            _sceneLoaderInstance = Instantiate(_sceneLoaderPrefab,transform);
        }
    }
}