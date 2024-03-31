using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTile : MonoBehaviour
{
    [SerializeField] private GameObject _ghostObject;
    [SerializeField] private GameObject _ghostArt;

    public void enableGhostArt()
    {
        _ghostArt.SetActive(true);
    }

    public void disableGhostArt()
    {
        if(_ghostObject != null)
        {
            Destroy(_ghostObject);
        }
        _ghostArt.SetActive(false);
    }

    public void generateGhostTile(PlaceData placeData)
    {
        if (_ghostObject != null)
        {
            Destroy(_ghostObject);
        }
        _ghostObject = Instantiate(placeData.ghostObject, _ghostArt.transform);
    }
}
