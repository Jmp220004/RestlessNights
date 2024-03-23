using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Tile Contents")]
    [SerializeField] private GameObject _placedObject;
    [SerializeField] private Placeable _placeable;
    [SerializeField] private Transform _placeablePosition; //The position placeable objects are moved to when the tile object is set
    [SerializeField] private GameObject _powerObject; //The power segment game object attached to this tile
    [SerializeField] private PowerSegment _powerSegment;
    [SerializeField] private Transform _powerPosition;
    [SerializeField] private SelectionStatus _selectionStatus;
    [Space]
    [Header("Fill References")]
    [SerializeField] private RelativeTiles _relativeTiles;

    /// <summary>
    /// Sets the tile's placed object variables based on the GameObject argument
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
            _placeable.CurrentTile = this;
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

    /// <summary>
    /// Sets the tile's placed power segment object based on the GameObject argument
    /// </summary>
    /// <param name="newPowerObject"></param>
    /// <returns>Returns -1 if the argument object does not have a powerline script attached. Otherwise returns 0</returns>
    public int setPowerObject(GameObject newPowerObject)
    {
        _powerObject = newPowerObject;
        _powerObject.transform.position = _powerPosition.transform.position;

        _powerSegment = _powerObject.GetComponent<PowerSegment>();

        if (_powerSegment == null)
        {
            return -1;
        }
        else
        {
            _powerSegment.CurrentTile = this;
        }

        return 0;
    }







    //GetSet
    public RelativeTiles getRelativeTiles()
    {
        return _relativeTiles;
    }
}
