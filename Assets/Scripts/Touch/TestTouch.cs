using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTouch : MonoBehaviour
{
    public TouchInputManager TIM;

    private void Start()
    {
        TIM.startHover += onHover;
        TIM.stopHover += onHoverEnd;
        TIM.releasedHover += onHoverRelease;
    }

    private void OnDestroy()
    {
        TIM.startHover -= onHover;
        TIM.stopHover -= onHoverEnd;
        TIM.releasedHover -= onHoverRelease;
    }

    private void onHover(GameObject hoverObject)
    {
        Debug.Log("Hover " + hoverObject.name);
    }

    private void onHoverEnd(GameObject hoverObject)
    {
        Debug.Log("Hover End " + hoverObject.name);
    }

    private void onHoverRelease(GameObject hoverObject)
    {
        Debug.Log("Release " + hoverObject.name);
    }
}
