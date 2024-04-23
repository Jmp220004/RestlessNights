using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSegment : MonoBehaviour
{
    [Header("Graphics settings")]
    [SerializeField] private Mesh _pipeMeshNone;
    [SerializeField] private Mesh _pipeMeshStraight;
    [SerializeField] private Mesh _pipeMeshCurved;
    [SerializeField] private Mesh _pipeMeshEnd;
    [Space]
    [Header("Necessary References")]
    [SerializeField] private MeshFilter _mf;
    [Space]
    [Header("Settings filled by code")]
    public PowerLine CurrentLine;
    public Tile CurrentTile;

    public void updateSegmentGraphics(int currentPosition)
    {
        if(CurrentLine != null)
        {
            direction previousDirection = CurrentLine.getPreviousTileRelativePosition(currentPosition);
            direction nextDirection = CurrentLine.getNextTileRelativePosition(currentPosition);

            //Pipe end code
            if(previousDirection == direction.none)
            {
                switch(nextDirection)
                {
                    case direction.none:
                        _mf.mesh = _pipeMeshNone;
                        break;
                    case direction.left:
                        _mf.mesh = _pipeMeshEnd;
                        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        break;
                    case direction.right:
                        _mf.mesh = _pipeMeshEnd;
                        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        break;
                    case direction.up:
                        _mf.mesh = _pipeMeshEnd;
                        transform.rotation = Quaternion.Euler(0f, 270f, 0f);
                        break;
                    case direction.down:
                        _mf.mesh = _pipeMeshEnd;
                        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                        break;
                }
                return;
            }

            if (nextDirection == direction.none)
            {
                switch (previousDirection)
                {
                    case direction.none:
                        _mf.mesh = _pipeMeshNone;
                        break;
                    case direction.left:
                        _mf.mesh = _pipeMeshEnd;
                        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        break;
                    case direction.right:
                        _mf.mesh = _pipeMeshEnd;
                        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        break;
                    case direction.up:
                        _mf.mesh = _pipeMeshEnd;
                        transform.rotation = Quaternion.Euler(0f, 270f, 0f);
                        break;
                    case direction.down:
                        _mf.mesh = _pipeMeshEnd;
                        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                        break;
                }
                return;
            }

            //Straight and curved pipe code pipe code
            if(previousDirection == direction.left)
            {
                switch(nextDirection)
                {
                    case direction.up:
                        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        _mf.mesh = _pipeMeshCurved;
                        break;
                    case direction.right:
                        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        _mf.mesh = _pipeMeshStraight;
                        break;
                    case direction.down:
                        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                        _mf.mesh = _pipeMeshCurved;
                        break;
                }
            }
            
            if(previousDirection == direction.up)
            {
                switch (nextDirection)
                {
                    case direction.left:
                        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        _mf.mesh = _pipeMeshCurved;
                        break;
                    case direction.right:
                        transform.rotation = Quaternion.Euler(0f, 270f, 0f);
                        _mf.mesh = _pipeMeshCurved;
                        break;
                    case direction.down:
                        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                        _mf.mesh = _pipeMeshStraight;
                        break;
                }
            }

            if (previousDirection == direction.right)
            {
                switch (nextDirection)
                {
                    case direction.up:
                        transform.rotation = Quaternion.Euler(0f, 270f, 0f);
                        _mf.mesh = _pipeMeshCurved;
                        break;
                    case direction.left:
                        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        _mf.mesh = _pipeMeshStraight;
                        break;
                    case direction.down:
                        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        _mf.mesh = _pipeMeshCurved;
                        break;
                }
            }

            if (previousDirection == direction.down)
            {
                switch (nextDirection)
                {
                    case direction.left:
                        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                        _mf.mesh = _pipeMeshCurved;
                        break;
                    case direction.right:
                        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        _mf.mesh = _pipeMeshCurved;
                        break;
                    case direction.up:
                        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                        _mf.mesh = _pipeMeshStraight;
                        break;
                }
            }
        }
    }
}
