using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyToSpawn;

    public SpawnerScriptableObject spawnerValues;

    int instanceNum = 1;

    void Start()
    {
        SpawnEntities();  
    }

    void SpawnEntities()
    {
        int currentSpawnPointIndex = 0;

        for (int i = 0; i < spawnerValues.numberOfSpawnsToCreate; i++)
        {
            GameObject currentEnemy = Instantiate(enemyToSpawn, 
                spawnerValues.spawnPoints[currentSpawnPointIndex], Quaternion.identity);
            
            currentEnemy.name = spawnerValues.spawner + instanceNum;

            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnerValues.spawnPoints.Length;

            instanceNum++;
        }
    }
}
