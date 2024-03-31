using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Action inventoryUpdated;

    [SerializeField] private int _startingGenerators;
    [SerializeField] private int _startingTowers;

    //ID 0 = generators
    //ID 1 = towers
    public int[] inventoryItems { get; private set; } = new int[2];

    private void Start()
    {
        inventoryItems[0] = _startingGenerators;
        inventoryItems[1] = _startingTowers;

        inventoryUpdated?.Invoke();
    }

    public void addInventoryValues(int inventoryID, int amount)
    {
        inventoryItems[inventoryID] += amount;
        inventoryUpdated?.Invoke();
    }
}
