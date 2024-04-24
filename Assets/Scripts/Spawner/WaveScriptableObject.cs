using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScripatbleObjects/WaveData", order = 2)]
public class WaveScriptableObject : ScriptableObject
{
    [Header("Randomized Spawns")]
    public bool RandomizeSpawnPoints;
    public int TotalRandomSpawns;
    [Space]
    [Header("Set Spawns")]
    public List<bool> EnabledSpawnPoints;
    [Space]
    [Header("Spawn Rate Variables")]
    public int TotalEnemies;
    public int EnemySpawnCooldown;
    public int MaximumSimultaneousSpawn;
    [Space]
    [Header("Inventory Settings")]
    public int WaveEndGenerators;
    public int WaveEndTowers;
    public int WaveEndPower;
}
