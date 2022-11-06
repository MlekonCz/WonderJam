using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class EnemyMover : MonoBehaviour
    {
        private List<Node> path = new List<Node>();
        [SerializeField] [Range(0f,10f)] private float speed = 1f;
        private Enemy enemy;
        private GridManager gridManager;
        private PathFinder pathFinder;
        

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
            gridManager = FindObjectOfType<GridManager>();
            pathFinder = FindObjectOfType<PathFinder>();
        }
        
        private void OnEnable()
        {
            ReturnToStart();
            RecalculatePath(true);
        }
        
        void RecalculatePath(bool resetPath)
        {
            Vector2Int coordinates = new Vector2Int();
            if (resetPath)
            {
                coordinates = pathFinder.StartCoordinates;
            }
            else
            {
                coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            }
            StopAllCoroutines();
            path.Clear();
            path = pathFinder.GetNewPath(coordinates);
            StartCoroutine(FollowPath());
        }

        void ReturnToStart()
        {
            transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
        }

        void FinishPath()
        {
            //  enemy.MoneyToSteal();
         //   gameObject.SetActive(false);
        }

        IEnumerator FollowPath()
        {
            for (int i = 1; i < path.Count; i++)
            {
                Vector3 startPosition = transform.position;
                Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
                float travelPercent = 0f;

                Vector3 difference = endPosition - startPosition;
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
             //   transform.LookAt(endPosition);
         
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