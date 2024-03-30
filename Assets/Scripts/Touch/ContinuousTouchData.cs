using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ContinuousTouchData
{
    public Vector2 lastTouchPosition = Vector2.zero;
    public Vector2 currentTouchPosition = Vector2.zero;
    public bool touchIsHeld = false;
    public float timeTouchHeld = 0f;
    public float timeSinceTouchLast = 0f;
}
