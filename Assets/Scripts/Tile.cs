using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Tile Contents")]
    [SerializeField] private GameObject _placedObject;
    [SerializeField] private Placeable _placeable;
    [SerializeField] private Transform _placeablePosition; //The position placeable objects are moved to when the tile object is set
    [SerializeField] private SelectionStatus _selectionStatus;
    [Space]
    [Header("Fill References")]
    [SerializeField] private RelativeTiles _relativeTiles;

    /// <summary>
    /// Sets the tile's placed object variables based on the game object argument
    /// </summary>
    /// <param name="newPlacedObject"></param>
    /// <returns>Returns -1 if the argument object does not have a placeable script attached. Otherwise returns 0</returns>
    public int setTileObject(GameObject newPlacedObject)
    {
        _placedObject = newPlacedObject;
        _placedObject.transform.position = _placeablePosition.transform.position;

        _placeable = _placedObject.GetComponent<Placeable>();

        if(_placeable == null)
        {
            return -1;
        }
        else
        {
            _placeable.currentTile = this;
        }

        return 0;
    }

    /// <summary>
    /// Clears the current object within the tile
    /// </summary>
    public void clearTile()
    {
        if(_placedObject != null)
        {
            Destroy(_placedObject);
        }

        _placedObject = null;
        _placeable = null;
    }
}
