using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

namespace GridSystem
{
    public class EnemyMover : MonoBehaviour
    {
        [FormerlySerializedAs("_Speed")] [SerializeField] [Range(0f,10f)] private float speed = 1f;
        private Enemy _enemy;

        private List<Waypoint> _waypoints = new List<Waypoint>();

        private PathController _pathController;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
            _pathController = FindObjectOfType<PathController>();
            _waypoints = _pathController.waypoints;
        }
        
        private void OnEnable()
        {
            ReturnToStart();
            StartCoroutine(FollowPath());

        }
        
        void ReturnToStart()
        {
            transform.position = _waypoints[0].transform.position;
        }

        void FinishPath()
        {
            //  enemy.MoneyToSteal();
            gameObject.SetActive(false);
        }

        IEnumerator FollowPath()
        {
            for (int i = 1; i < _waypoints.Count; i++)
            {
                Vector3 startPosition = transform.position;
                Vector3 endPosition = _waypoints[i].transform.position;
                float travelPercent = 0f;

                Vector3 difference = endPosition - startPosition;
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
                
                while (travelPercent <1f )
                {
                    travelPercent += Time.deltaTime * speed;
                    transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                    yield return new WaitForEndOfFrame();
                }
            }
            FinishPath();
        }

    }
}