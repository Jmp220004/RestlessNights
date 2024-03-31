using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : StateMachineMB
{
    [Header("Variables")]
    public bool CursorEnabled;
    public PlaceData HeldObject;

    [Header("References")]
    public TouchInputManager Tim;
    public Image CursorImage;
    public GameObject CursorArtObject;
    public Inventory Inventory;

    // State instances
    public CursorTowerState towerState = new CursorTowerState();


    //Set up C# events related to the touch manager
    private void Awake()
    {
        ChangeState(towerState);

        Tim.startHover += onHover;
        Tim.stopHover += onStopHover;
        Tim.releasedHover += onRelease;
        Tim.startUIHover += onStartUIHover;
    }

    //Unsubscribe from the touch manager once the cursor object is destroyed
    protected override void OnDestroy()
    {
        base.OnDestroy();

        Tim.startHover -= onHover;
        Tim.stopHover -= onStopHover;
        Tim.releasedHover -= onRelease;
        Tim.startUIHover -= onStartUIHover;
    }

    private void Start()
    {
        //Check the initial cursor enabled state
        if(CursorEnabled)
        {
            enableCursor();
        }
        else
        {
            disableCursor();
        }
    }

    protected override void Update()
    {
        base.Update();

        gameObject.transform.position = Tim.continuousTouchData.currentTouchPosition;
    }

    /// <summary>
    /// Activates when the player is holding the cursor over a tile
    /// </summary>
    public void onHover(GameObject tileObject)
    {
        if(CurrentState is ICursorState)
        {
            ICursorState thisState = (ICursorState)CurrentState;
            thisState.onHover(tileObject, this);
        }
    }

    /// <summary>
    /// Activates when the player stops holding the cursor over a tile but is still holding town the button
    /// </summary>
    public void onStopHover(GameObject tileObject)
    {
        if (CurrentState is ICursorState)
        {
            ICursorState thisState = (ICursorState)CurrentState;
            thisState.onStopHover(tileObject, this);
        }
    }

    /// <summary>
    /// Activates when the player releases their finger input over a tile
    /// </summary>
    public void onRelease(GameObject tileObject)
    {
        if (CurrentState is ICursorState)
        {
            ICursorState thisState = (ICursorState)CurrentState;
            thisState.onRelease(tileObject, this);
        }
    }

    /// <summary>
    /// Activates when the player starts holding over a selectable UI element
    /// </summary>
    public void onStartUIHover(GameObject uiHoverObject)
    {
        if (CurrentState is ICursorState)
        {
            ICursorState thisState = (ICursorState)CurrentState;
            thisState.onStartUIHover(uiHoverObject, this);
        }
    }

    public void disableCursor()
    {
        CursorArtObject.SetActive(false);
    }

    public void enableCursor()
    {
        CursorArtObject.SetActive(true);
        CursorImage.sprite = HeldObject.objectSpr;
    }

    public void enableCursor(Sprite newSprite)
    {
        CursorArtObject.SetActive(true);
        CursorImage.sprite = newSprite;
    }
}