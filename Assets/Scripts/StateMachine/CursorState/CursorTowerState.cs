using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTowerState : State, ICursorState
{
    /// <summary>
    /// Activates when the player is holding the cursor over a tile
    /// </summary>
    public void onHover(GameObject tileObject, Cursor inCursor)
    {
        //Code the handles moving tiles when clicked
        if (inCursor.HeldObject == null)
        {
            Tile attemptedMoveTile = tileObject.GetComponent<Tile>();
            if (attemptedMoveTile != null)
            {
                if (attemptedMoveTile.Occupied)
                {
                    inCursor.HeldObject = attemptedMoveTile.PlaceData;
                    attemptedMoveTile.clearTile();
                }
            }
        }
        else
        {
            //Ghost hover code
            Tile attemptedGhostTile = tileObject.GetComponent<Tile>();
            if (attemptedGhostTile != null && attemptedGhostTile.Occupied == false)
            {
                //Ghost hover code
                attemptedGhostTile.getGhostTile().generateGhostTile(inCursor.HeldObject);
                attemptedGhostTile.getGhostTile().enableGhostArt();
            }
        }

        inCursor.disableCursor();
    }

    /// <summary>
    /// Activates when the player stops holding the cursor over a tile but is still holding town the button
    /// </summary>
    public void onStopHover(GameObject tileObject, Cursor inCursor)
    {
        if (inCursor.HeldObject != null)
        {
            inCursor.enableCursor();

            //Ghost hover code
            Tile attemptedGhostTile = tileObject.GetComponent<Tile>();
            if (attemptedGhostTile != null)
            {
                //Ghost hover code
                attemptedGhostTile.getGhostTile().disableGhostArt();
            }
        }
    }

    /// <summary>
    /// Activates when the player releases their finger input over a tile
    /// </summary>
    public void onRelease(GameObject tileObject, Cursor inCursor)
    {
        if (inCursor.HeldObject != null && tileObject != null)
        {
            Tile attemptedPlacementTile = tileObject.GetComponent<Tile>();
            if (attemptedPlacementTile != null && attemptedPlacementTile.Occupied == false)
            {
                //Spawn the placeable object from the heldObject's placement data
                GameObject spawnedPlaceableObj = UnityEngine.Object.Instantiate(inCursor.HeldObject.placeObject, Vector3.zero, Quaternion.identity);
                //Check to see if the placement is valid
                int validityCheck = attemptedPlacementTile.setTileObject(spawnedPlaceableObj);
                //If the placement is not valid, despawn the spawned object
                if (validityCheck == -1)
                {
                    UnityEngine.Object.Destroy(spawnedPlaceableObj);
                    //If the object can't be placed, put it back in the inventory
                    inCursor.Inventory.addInventoryValues(inCursor.HeldObject.inventoryID, 1);
                }
                else
                {
                    //If it was valid, set the placement data of the tile to the current heldObject
                    attemptedPlacementTile.PlaceData = inCursor.HeldObject;
                    //Delete the ghost version of the object as well
                    attemptedPlacementTile.getGhostTile().disableGhostArt();
                }
            }
            else
            {
                //If the object can't be placed, put it back in the inventory
                inCursor.Inventory.addInventoryValues(inCursor.HeldObject.inventoryID, 1);
            }
        }
        else
        {
            //If the object can't be placed, put it back in the inventory
            if (inCursor.HeldObject != null)
            {
                inCursor.Inventory.addInventoryValues(inCursor.HeldObject.inventoryID, 1);
            }
        }

        inCursor.HeldObject = null;
        inCursor.disableCursor();
    }

    /// <summary>
    /// Activates when the player starts holding over a selectable UI element
    /// </summary>
    public void onStartUIHover(GameObject uiHoverObject, Cursor inCursor)
    {
        //Checks to see if the player is hovering over a dragable UI button. If so, enable the cursor and set the held data to the button's placeable data.
        DragButton drag = uiHoverObject.GetComponent<DragButton>();
        if (drag != null && inCursor.HeldObject == null)
        {
            //Check inventory requirements
            if (inCursor.Inventory.inventoryItems[drag.buttonPlaceable.inventoryID] > 0)
            {
                inCursor.Inventory.addInventoryValues(drag.buttonPlaceable.inventoryID, -1);
                inCursor.HeldObject = drag.buttonPlaceable;
                inCursor.enableCursor();
            }
        }
    }

    /// <summary>
    /// Activates when the player stops holding over a selectable UI element
    /// </summary>
    public void onReleaseUIHover(GameObject uiHoverObject, Cursor inCursor)
    {

    }

    //Activates when the player tables the toggle cable button
    public void onToggleCablePress(Cursor inCursor)
    {
        inCursor.ChangeState(inCursor.cableState);
    }
}
