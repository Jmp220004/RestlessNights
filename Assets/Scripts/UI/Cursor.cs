using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private bool _cursorEnabled;
    [SerializeField] private PlaceData _heldObject;

    [Header("References")]
    [SerializeField] private TouchInputManager _tim;
    [SerializeField] private Image _cursorImage;
    [SerializeField] private GameObject _cursorArtObject;

    //Set up C# events related to the touch manager
    private void Awake()
    {
        _tim.startHover += onHover;
        _tim.stopHover += onStopHover;
        _tim.releasedHover += onRelease;
        _tim.startUIHover += onStartUIHover;
    }

    //Unsubscribe from the touch manager once the cursor object is destroyed
    private void OnDestroy()
    {
        _tim.startHover -= onHover;
        _tim.stopHover -= onStopHover;
        _tim.releasedHover -= onRelease;
        _tim.startUIHover -= onStartUIHover;
    }

    private void Start()
    {
        //Check the initial cursor enabled state
        if(_cursorEnabled)
        {
            enableCursor();
        }
        else
        {
            disableCursor();
        }
    }

    private void Update()
    {
        gameObject.transform.position = _tim.continuousTouchData.currentTouchPosition;
    }

    /// <summary>
    /// Activates when the player is holding the cursor over a tile
    /// </summary>
    /// <param name="hoverObject"></param>
    private void onHover(GameObject tileObject)
    {
        //Code the handles moving tiles when clicked
        if (_heldObject == null)
        {
            Tile attemptedMoveTile = tileObject.GetComponent<Tile>();
            if (attemptedMoveTile != null)
            {
                if(attemptedMoveTile.Occupied)
                {
                    _heldObject = attemptedMoveTile.PlaceData;
                    attemptedMoveTile.clearTile();
                }
            }

        }

        disableCursor();
    }

    /// <summary>
    /// Activates when the player stops holding the cursor over a tile but is still holding town the button
    /// </summary>
    /// <param name="hoverObject"></param>
    private void onStopHover(GameObject hoverObject)
    {
        if(_heldObject != null)
        {
            enableCursor();
        }
    }

    /// <summary>
    /// Activates when the player releases their finger input over a tile
    /// </summary>
    /// <param name="hoverObject"></param>
    private void onRelease(GameObject tileObject)
    {
        if (_heldObject != null && tileObject != null)
        {
            Tile attemptedPlacementTile = tileObject.GetComponent<Tile>();
            if (attemptedPlacementTile != null && attemptedPlacementTile.Occupied == false)
            {
                //Spawn the placeable object from the heldObject's placement data
                GameObject spawnedPlaceableObj = Instantiate(_heldObject.placeObject, Vector3.zero, Quaternion.identity);
                //Check to see if the placement is valid
                int validityCheck = attemptedPlacementTile.setTileObject(spawnedPlaceableObj);
                //If the placement is not valid, despawn the spawned object
                if (validityCheck == -1)
                {
                    Destroy(spawnedPlaceableObj);
                }
                else
                {
                    //If it was valid, set the placement data of the tile to the current heldObject
                    attemptedPlacementTile.PlaceData = _heldObject;
                }
            }
        }

        _heldObject = null;
        disableCursor();
    }

    /// <summary>
    /// Activates when the player starts holding over a selectable UI element
    /// </summary>
    /// <param name="uiHoverObject"></param>
    private void onStartUIHover(GameObject uiHoverObject)
    {
        //Checks to see if the player is hovering over a dragable UI button. If so, enable the cursor and set the held data to the button's placeable data.
        DragButton drag = uiHoverObject.GetComponent<DragButton>();
        if(drag != null && _heldObject == null)
        {
            _heldObject = drag.buttonPlaceable;
            enableCursor();
        }
    }

    private void disableCursor()
    {
        _cursorArtObject.SetActive(false);
    }

    private void enableCursor()
    {
        _cursorArtObject.SetActive(true);
        _cursorImage.sprite = _heldObject.objectSpr;
    }

    private void enableCursor(Sprite newSprite)
    {
        _cursorArtObject.SetActive(true);
        _cursorImage.sprite = newSprite;
    }
}
