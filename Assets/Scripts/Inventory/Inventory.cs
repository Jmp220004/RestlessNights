using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Action inventoryUpdated;

    [SerializeField] private int _startingGenerators;
    [SerializeField] private int _startingTowers;
    [SerializeField] private int _startingCables;

    private WaveScriptableObject _currentWave;
    private GameFSM _gameFSM;


    //ID 0 = default generators
    //ID 1 = default towers
    //ID 2 = cables

    public int[] inventoryItems { get; private set; } = new int[3];


    private void Awake()
    {
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


    private void Start()
    {
        inventoryItems[0] = _startingGenerators;
        inventoryItems[1] = _startingTowers;
        inventoryItems[2] = _startingCables;

        inventoryUpdated?.Invoke();
    }

    public void addInventoryValues(int inventoryID, int amount)
    {
        inventoryItems[inventoryID] += amount;
        inventoryUpdated?.Invoke();
    }

    public void OnGameStateChange(string newStateName)
    {
        switch (newStateName)
        {
            case "GamePlacementState":
                _currentWave = _gameFSM.getCurrentWaveData();
                inventoryItems[0] += _currentWave.WaveEndGenerators;
                inventoryItems[1] += _currentWave.WaveEndTowers;
                inventoryItems[2] += _currentWave.WaveEndPower;
                inventoryUpdated?.Invoke();
                break;

            case "GameWaveState":
                break;
        }
    }
}
