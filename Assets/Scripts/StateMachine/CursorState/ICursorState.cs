using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICursorState
{
    void onHover(GameObject tileObject, Cursor inCursor);
    void onStopHover(GameObject tileObject, Cursor inCursor);
    void onRelease(GameObject tileObject, Cursor inCursor);
    void onStartUIHover(GameObject uiHoverObject, Cursor inCursor);
}
