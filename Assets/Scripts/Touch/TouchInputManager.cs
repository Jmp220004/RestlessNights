using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInputManager : MonoBehaviour
{
    public ContinuousTouchData continuousTouchData;

    public GameObject hoveredObject;

    public Action<Vector2> tap;
    public Action<Vector2> hold;
    public Action<GameObject> startHover;
    public Action<GameObject> stopHover;
    public Action<GameObject> releasedHover;

    [Header("Interaction Settings")]
    [SerializeField] private float _pressHeldThreshold;
    [SerializeField] private LayerMask _touchableLayermask;

    //Fill fields
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    protected virtual void Update() 
    {
        //Handle touch held code
        if(continuousTouchData.touchIsHeld)
        {
            continuousTouchData.timeTouchHeld += Time.deltaTime;
        }
        continuousTouchData.timeSinceTouchLast += Time.deltaTime;

    }

    public virtual void touchPressInputAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            continuousTouchData.touchIsHeld = true;
        }
        
        if (context.canceled)
        {
            continuousTouchData.lastTouchPosition = continuousTouchData.currentTouchPosition;
            continuousTouchData.touchIsHeld = false;
            //Action code

            //Hold action
            if (continuousTouchData.timeTouchHeld >= _pressHeldThreshold)
            {
                hold?.Invoke(continuousTouchData.currentTouchPosition);
            }
            else
            {
                tap?.Invoke(continuousTouchData.currentTouchPosition);
            }

            continuousTouchData.timeTouchHeld = 0;
            continuousTouchData.timeSinceTouchLast = 0;

            //Execute the release code if there's currently a hovered object
            if(hoveredObject)
            {
                releasedHover?.Invoke(hoveredObject);
                hoveredObject = null;
            }
        }
    }

    public virtual void touchPositionInputAction(InputAction.CallbackContext context)
    {
        if(continuousTouchData.touchIsHeld && context.performed)
        {
            continuousTouchData.currentTouchPosition = context.ReadValue<Vector2>();

            //Check if the current player tap is hovering over an object
            Ray ray = mainCamera.ScreenPointToRay(continuousTouchData.currentTouchPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Check if the game object has a collider and that the layer is within the touchable layer mask
                if (hit.collider != null && _touchableLayermask == (_touchableLayermask | (1 << hit.collider.gameObject.layer)))
                {
                    //Change the hovering object if the new hovering object and fire the event
                    if(hit.collider.gameObject != hoveredObject)
                    {
                        if(hoveredObject != null)
                        {
                            stopHover?.Invoke(hoveredObject);
                        }
                        hoveredObject = hit.collider.gameObject;
                        startHover?.Invoke(hit.collider.gameObject);
                    }
                }
                else
                {
                    //If the raycast is either no longer hitting an object or not hitting an object with a collider, invoke the stop hover event while passing the object that stopped hovering
                    if(hoveredObject != null)
                    {
                        stopHover?.Invoke(hoveredObject);
                        hoveredObject = null;
                    }
                }
            }
            else
            {
                if (hoveredObject != null)
                {
                    stopHover?.Invoke(hoveredObject);
                    hoveredObject = null;
                }
            }
        }
    }
}
