using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAddSegment : MonoBehaviour
{
    public Tile Tile1;
    public Tile Tile2;
    public Tile Tile3;

    public PowerLine PowerLine;

    public PowerSegment Segment1;
    public PowerSegment Segment2;
    public PowerSegment Segment3;

    private void Start()
    {
        Debug.Log(PowerLine.attemptAddSegment(Segment1, Tile1));
        Debug.Log(PowerLine.attemptAddSegment(Segment2, Tile2));
        Debug.Log(PowerLine.attemptAddSegment(Segment3, Tile3));
    }
}
