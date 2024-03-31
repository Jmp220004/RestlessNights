using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCableButton : MonoBehaviour
{
    [SerializeField] private Cursor _cursor;
    [SerializeField] private GameObject _uiHighlight;

    public void enableHighlight()
    {
        _uiHighlight.SetActive(true);
    }

    public void disableHighlight()
    {
        _uiHighlight.SetActive(false);
    }

    public void onClick()
    {
        _cursor.onToggleCablePress();
    }
}
