using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScripatbleObjects/SpawnerScriptableObject", order = 1)]
public class SpawnerScriptableObject : ScriptableObject
{
    public string spawner;

    public GameObject warning;

    public int numberOfSpawnsLocations;
    public Vector3[] spawnPoints;
}
