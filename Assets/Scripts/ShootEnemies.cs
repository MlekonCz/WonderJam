using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class ShootEnemies : MonoBehaviour
{
    public List<GameObject> enemiesInRange = new List<GameObject>();

// Start is called before the first frame update
     private void OnEnemyDestroy(GameObject enemy)
        {
            enemiesInRange.Remove (enemy);
        }

     private  void OnTriggerEnter2D (Collider2D other)
        {
            if (other.gameObject.tag.Equals("Enemy"))
            {
                enemiesInRange.Add(other.gameObject);
                EnemyDestructionDelegate del =
                    other.gameObject.GetComponent<EnemyDestructionDelegate>();
                del.enemyDelegate += OnEnemyDestroy;
            }
        }
     private  void OnTriggerExit2D (Collider2D other)
        {
            if (other.gameObject.tag.Equals("Enemy"))
            {
                enemiesInRange.Remove(other.gameObject);
                EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
                del.enemyDelegate -= OnEnemyDestroy;
            }
        }
}
