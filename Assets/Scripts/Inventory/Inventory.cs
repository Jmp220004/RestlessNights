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

    //ID 0 = default generators
    //ID 1 = default towers
    //ID 2 = cables

    public int[] inventoryItems { get; private set; } = new int[3];

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
}
