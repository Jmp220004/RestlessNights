using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCableState : State, ICursorState
{
    private Cursor _cursor;
    private PowerLine _selectedLine;

    public CursorCableState(Cursor thisCursor)
    {
        _cursor = thisCursor;
        _selectedLine = null;
    }

    public override void Enter()
    {
        base.Enter();

        _cursor.cableButton.enableHighlight();
    }

    public override void Exit()
    {
        base.Exit();

        _cursor.cableButton.disableHighlight();
    }

    /// <summary>
    /// Activates when the player is holding the cursor over a tile
    /// </summary>
    public void onHover(GameObject tileObject, Cursor inCursor)
    {
        Tile attemptedCableTile = tileObject.GetComponent<Tile>();
        if(attemptedCableTile != null)
        {
            if(_selectedLine == null)
            {
                if(attemptedCableTile.HasPowerObject)
                {
                    //Clear the line if the selected tile currently has a line and the player is not currently trying to place a line
                    int removedCables = attemptedCableTile.getPowerSegment().CurrentLine.clearLine();
                    inCursor.Inventory.addInventoryValues(2, removedCables);
                }
                else if(inCursor.Inventory.inventoryItems[2] > 0)
                {
                    //Create a new line if the selected tile is empty and the player is not currently creating a line
                    GameObject powerlineObject = UnityEngine.Object.Instantiate(inCursor.PowerlinePrefab, Vector3.zero, Quaternion.identity);
                    GameObject powersegmentObject = UnityEngine.Object.Instantiate(inCursor.PowerSegmentPrefab, Vector3.zero, Quaternion.identity);

                    _selectedLine = powerlineObject.GetComponent<PowerLine>();
                    PowerSegment spawnedSegment = powersegmentObject.GetComponent<PowerSegment>();
                    
                    if(_selectedLine != null && spawnedSegment != null)
                    {
                        _selectedLine.attemptAddSegment(spawnedSegment, attemptedCableTile);
                        inCursor.Inventory.addInventoryValues(2, -1);
                    }
                }
            }
            else if(attemptedCableTile.HasPowerObject == false && inCursor.Inventory.inventoryItems[2] > 0)
            {
                //If the player is creating a line, keep trying to add power segments to the line's end on the hovered tile
                GameObject powersegmentObject = UnityEngine.Object.Instantiate(inCursor.PowerSegmentPrefab, Vector3.zero, Quaternion.identity);

                PowerSegment spawnedSegment = powersegmentObject.GetComponent<PowerSegment>();

                if(spawnedSegment != null)
                {
                    int validityCheck = _selectedLine.attemptAddSegment(spawnedSegment, attemptedCableTile, lineEnd.end);
                    if(validityCheck == -1)
                    {
                        //If the validity check is not valid (-1) destroy the spawned power segment
                        UnityEngine.Object.Destroy(powersegmentObject);
                    }
                    else
                    {
                        //If the validity check passes, remove a cable from the inventory
                        inCursor.Inventory.addInventoryValues(2, -1);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Activates when the player stops holding the cursor over a tile but is still holding town the button
    /// </summary>
    public void onStopHover(GameObject tileObject, Cursor inCursor)
    {

    }

    /// <summary>
    /// Activates when the player releases their finger input over a tile
    /// </summary>
    public void onRelease(GameObject tileObject, Cursor inCursor)
    {
        //Stop selecting a line when the player stops holding down on the screen
        _selectedLine = null;
    }

    /// <summary>
    /// Activates when the player starts holding over a selectable UI element
    /// </summary>
    public void onStartUIHover(GameObject uiHoverObject, Cursor inCursor)
    {
        //Checks to see if the player is hovering over a dragable UI button. If so, switch to the tower UI
        DragButton drag = uiHoverObject.GetComponent<DragButton>();
        if (drag != null && inCursor.HeldObject == null)
        {
            inCursor.ChangeState(inCursor.towerState);
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
        inCursor.ChangeState(inCursor.towerState);
    }
}
