using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCableState : State, ICursorState
{
    private Cursor _cursor;

    public CursorCableState(Cursor thisCursor)
    {
        _cursor = thisCursor;
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
