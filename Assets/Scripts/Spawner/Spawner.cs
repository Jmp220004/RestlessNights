using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyToSpawn;

    public SpawnerScriptableObject spawnerValues;

    int instanceNum = 1;

    private float _elapsedCooldown = 0;

    private GameFSM _gameFSM;
    public int spawnLimit = 4;
    public float SpawnCooldown = 2;
    public int EnemiesKilled;
    public bool CanSpawn;


    private void Start()
    {
        StartCoroutine(RowFlash());
    }
    private void Awake()
    {
        EnemiesKilled = 0;

        _gameFSM = GameObject.FindGameObjectWithTag("GameFSM").GetComponent<GameFSM>();

        if (_gameFSM != null)
        {
            _gameFSM.OnStateChange += OnGameStateChange;
        }

    }

    private void OnDestroy()
    {
        _gameFSM.OnStateChange -= OnGameStateChange;
    }

    private void FixedUpdate()
    {
        if (EnemiesKilled >= instanceNum)
        {
            EndWave();
        }
        _elapsedCooldown += Time.deltaTime;
        if (_elapsedCooldown >= SpawnCooldown && CanSpawn == true && instanceNum<spawnLimit)
        {

            SpawnEntities();

            _elapsedCooldown = 0;
        }

    }

        void SpawnEntities()
    {
        int currentSpawnPointIndex = 0;

        for (int i = 0; i < spawnerValues.numberOfSpawnsLocations; i++)
        {
            GameObject currentEnemy = Instantiate(enemyToSpawn, 
                spawnerValues.spawnPoints[currentSpawnPointIndex], Quaternion.identity);
            
            currentEnemy.name = spawnerValues.spawner + instanceNum;

            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnerValues.spawnPoints.Length;

            instanceNum++;
        }
    }

    IEnumerator RowFlash()
    {
        int currentSpawnPointIndex = 0;

        for (int i = 0; i < spawnerValues.numberOfSpawnsLocations; i++)
        {
            Instantiate(spawnerValues.warning, spawnerValues.spawnPoints[currentSpawnPointIndex], transform.rotation);
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnerValues.spawnPoints.Length;
        }
        yield return new WaitForSeconds(5f);
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Warning");
        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }

    }

    public void EndWave()
    {
        Debug.Log("changing to placement state");
        _gameFSM.ChangeState(_gameFSM.PlacementState);
        StartCoroutine(RowFlash());

    }

    public void AddEnemyKilled(int number)
    {
        EnemiesKilled += number;
    }
    public void OnGameStateChange(string newStateName)
    {
        Debug.Log(newStateName);
        switch (newStateName)
        {
            case "GamePlacementState":
                CanSpawn = false;
                EnemiesKilled = 0;
                break;

            case "GameWaveState":
                CanSpawn = true;
                break;
        }
    }
}
