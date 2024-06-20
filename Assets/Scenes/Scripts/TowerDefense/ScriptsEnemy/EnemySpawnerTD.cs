using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnSettings
{
    public GameObject enemyPrefab;
    public float spawnInterval;
}
public class EnemySpawnerTD : MonoBehaviour
{
    public EnemySpawnSettings[] spawnSettings;
    public Transform[] waypoints;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    IEnumerator SpawnEnemies()
    {
        
        for (int i = 0; i < spawnSettings.Length; i++)
        {
            
            SpawnEnemiesForSettings(spawnSettings[i].enemyPrefab);
            // Ждем перед следующим спавном
            yield return new WaitForSeconds(spawnSettings[i].spawnInterval);
        }

        
        yield return null;
    }
    void SpawnEnemiesForSettings(GameObject enemyPrefab)
    {
        
            
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            Vector3 spawnPosition = newEnemy.transform.position;
            spawnPosition.y = 0;
            newEnemy.transform.position = spawnPosition;
            EnemyParametrs enemyMovement = newEnemy.GetComponent<EnemyParametrs>();
            if (enemyMovement != null)
            {
                enemyMovement.SetWaypoints(waypoints);
            }

            
        
    }
}
