using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [Header("Dependencies")]

    public int _spawnLimit = 4;
    public int enemies = 0;
    public int EnemiesKilled;
    GameObject clone;
    GameObject laneClone;

    public bool CanSpawn;

    [Header("General")]
    public float SpawnCooldown = 2;

    [SerializeField] GameObject _flashRow;
    [SerializeField] List<GameObject> _spawnList = new List<GameObject>();
    [SerializeField] List<Transform> _spawnArea = new List<Transform>();

    private float _elapsedCooldown = 0;

    private GameFSM _gameFSM;

    private Transform spawn;
    private GameObject enemy;


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
        if (EnemiesKilled >= _spawnLimit)
        {
            EndWave();
        }

        _elapsedCooldown += Time.deltaTime;
        if (_elapsedCooldown >= SpawnCooldown && CanSpawn == true)
        {

            Spawn();
            
            _elapsedCooldown = 0;
        }
    }

    // Spawn Location
    public static Transform GetRandomLocation(List<Transform> spawnArea)
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnArea.Count);
        return spawnArea[randomIndex];
    }



    // For Multiple enemy implementation
    
    public static GameObject GetRandomSpawn(List<GameObject> spawnList)
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnList.Count);
        return spawnList[randomIndex];
    }


    
    public void Spawn()
    {
        if (_spawnList.Count == 0 || _spawnList == null) { return; }
        if (_spawnArea.Count == 0 || _spawnList == null) { return; }
        spawn = GetRandomLocation(_spawnArea);
        enemy = GetRandomSpawn(_spawnList);
        if (enemies < _spawnLimit)
        {
            clone = Instantiate(enemy, spawn.position, transform.rotation);
            enemies++;
        }
        killed();
        
        
        // make it harder, but not below a certain amount
        if (SpawnCooldown > .1f)
        {
            SpawnCooldown -= .02f;
        }
        else
        {
            SpawnCooldown = .1f;
        }
    }
    public void killed()
    {
        if(clone == null)
        {
           // Debug.Log("Dead");
            enemies = 0;
        }
    }

    IEnumerator RowFlash()
    {
        for (int i = 0; i < _spawnArea.Count; i++)
        {
            laneClone = Instantiate(_flashRow, _spawnArea[i].position, transform.rotation);
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
