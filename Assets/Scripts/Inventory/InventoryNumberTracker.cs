using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryNumberTracker : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private int _trackedInventorySlot;
    [SerializeField] private TMP_Text _textField;

    private void Awake()
    {
        _inventory.inventoryUpdated += onInventoryUpdate;
    }

    private void onInventoryUpdate()
    {
        _textField.SetText(_inventory.inventoryItems[_trackedInventorySlot].ToString());
    }
}
