using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyToSpawn;

    public WaveScriptableObject CurrentWave;

    private float _elapsedCooldown = 0;

    private GameFSM _gameFSM;

    public int EnemiesSpawnedWave;
    public int EnemiesKilledWave;
    public List<Transform> spawnLocations;
    public List<bool> EnabledSpawners = new List<bool> { false, false, false, false, false };
    [SerializeField] private List<GameObject> _warningIndicators;
    public bool CanSpawn;

    private void Awake()
    {
        EnemiesKilledWave = 0;

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
        if(CanSpawn)
        {
            if (EnemiesKilledWave >= CurrentWave.TotalEnemies)
            {
                EndWave();
            }
            else
            {
                _elapsedCooldown += Time.fixedDeltaTime;
                if(_elapsedCooldown >= CurrentWave.EnemySpawnCooldown)
                {
                    SpawnEnemies();
                    _elapsedCooldown = 0;
                }
            }
        }
    }

    private int pickRandomSpawnLane(List<int> excludeIndeces)
    {
        List<int> enabledSpawnersIndices = new List<int>();
        //Check which indexes are enabled
        for (int i = 0; i < 5; i++)
        {
            if (EnabledSpawners[i] == true && excludeIndeces.Contains(i) != true)
            {
                enabledSpawnersIndices.Add(i);
            }
        }

        if (enabledSpawnersIndices.Count == 0)
        {
            return -1;
        }

        //Pick a random index from the available spawns
        int randomIndex = Random.Range(0, enabledSpawnersIndices.Count);

        return enabledSpawnersIndices[randomIndex];
    }

    private void SpawnEnemies()
    {
        List<int> usedSpawners = new List<int>();
        int numberOfEnemiesToSpawn = Random.Range(1, CurrentWave.MaximumSimultaneousSpawn + 1);

        for(int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            int pickedLane = pickRandomSpawnLane(usedSpawners);

            if(pickedLane != -1 && EnemiesSpawnedWave < CurrentWave.TotalEnemies && usedSpawners.Contains(pickedLane) == false)
            {
                Vector3 laneTransformPosition = spawnLocations[pickedLane].position;
                Instantiate(enemyToSpawn, laneTransformPosition, Quaternion.identity);

                usedSpawners.Add(pickedLane);
                EnemiesSpawnedWave++;
            }
        }

    }

    private void getWaveEnabledLanes()
    {
        if(CurrentWave.RandomizeSpawnPoints == false)
        {
            EnabledSpawners = CurrentWave.EnabledSpawnPoints;
        }
        else
        {
            //Reset the enabled spawners list
            EnabledSpawners = new List<bool> { false, false, false, false, false };

            //Enable random indeces based on how many are meant to be added
            int totalLanes = 0;
            List<int> excludeIndices = new List<int>();
            while (totalLanes < CurrentWave.TotalRandomSpawns)
            {
                int randomLane = Random.Range(0, 5);
                if(excludeIndices.Contains(randomLane) == false)
                {
                    EnabledSpawners[randomLane] = true;
                    excludeIndices.Add(randomLane);
                    totalLanes++;
                }
            }
        }
    }

    private void RowWarningStart()
    {
        for(int i = 0; i < 5; i++)
        {
            if(EnabledSpawners[i] == true)
            {
                _warningIndicators[i].SetActive(true);
            }
            else
            {
                _warningIndicators[i].SetActive(false);
            }
        }
    }

    private void RowWarningEnd()
    {
        for (int i = 0; i < 5; i++)
        {
            _warningIndicators[i].SetActive(false);
        }
    }

    public void EndWave()
    {
        Debug.Log("Changing to placement state");
        _gameFSM.ChangeState(_gameFSM.PlacementState);
    }

    public void AddEnemyKilled(int number)
    {
        EnemiesKilledWave += number;
    }
    public void OnGameStateChange(string newStateName)
    {
        switch (newStateName)
        {
            case "GamePlacementState":
                CanSpawn = false;
                EnemiesSpawnedWave = 0;
                EnemiesKilledWave = 0;
                CurrentWave = _gameFSM.getCurrentWaveData();
                getWaveEnabledLanes();
                RowWarningStart();
                break;

            case "GameWaveState":
                CanSpawn = true;
                _elapsedCooldown = 0;
                RowWarningEnd();
                break;
        }
    }
}
