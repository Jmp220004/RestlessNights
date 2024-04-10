using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [Header("Dependencies")]

    private int _spawnLimit = 4;
    public int enemies = 0;
    GameObject clone; 

    

    [Header("General")]
    public float SpawnCooldown = 2;

    [SerializeField] List<GameObject> _spawnList = new List<GameObject>();
    [SerializeField] List<Transform> _spawnArea = new List<Transform>();

    private float _elapsedCooldown = 0;



    private void Update()
    {
        _elapsedCooldown += Time.deltaTime;
        if (_elapsedCooldown >= SpawnCooldown)
        {
            Spawn();
            
            _elapsedCooldown = 0;
        }
    }

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
        if (_spawnArea.Count == 0 || _spawnList == null) { return; }
        if (_spawnList.Count == 0 || _spawnList == null) { return; }
        GameObject randomSpawnObject = GetRandomSpawn(_spawnList);
        Transform randomSpawnArea = GetRandomLocation(_spawnArea);
        if (enemies <= _spawnLimit)
        {
            clone = Instantiate(randomSpawnObject, randomSpawnArea.position, transform.rotation);
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
            Debug.Log("Dead");
            enemies = 0;
        }
    }
}
