using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _meleeEnemy;
    [SerializeField] private GameObject _rangedEnemy;

    [SerializeField] private float _meleeInterval = 3.5f;
    [SerializeField] private float _rangedInterval = 6f;

    [SerializeField] private int _enemiesToSpawn = 5;
    private int _enemiesSpawned;


    // Start is called before the first frame update
    void Start()
    {
        _enemiesSpawned = 1;
        StartCoroutine(spawnEnemy(_meleeInterval, _meleeEnemy));
        StartCoroutine(spawnEnemy(_rangedInterval, _rangedEnemy));
    }

    private void Update()
    {

    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        if (_enemiesSpawned <= _enemiesToSpawn)
        {
            GameObject newEnemy = Instantiate(enemy, transform.localPosition, Quaternion.identity);
            StartCoroutine(spawnEnemy(interval, enemy));
            _enemiesSpawned += 1;

        }

    }
}
