using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum lineEnd
{
    beginning,
    end
}

public class PowerLine : MonoBehaviour
{
    public List<PowerSegment> _powerSegments;

    /// <summary>
    /// Attempts to add a power segment to the specified tile. Will fail if the power segment is not the beginning of the chain or is not adjacent to the end of the chain.
    /// </summary>
    /// <param name="segmentToAdd">The segment being added to the tile</param>
    /// <param name="tileToAddSegment">The tile the segment is being placed on</param>
    /// <returns>Returns -1 if the argument tile is a valid addition to the list. Otherwise returns 0</returns>
    public int attemptAddSegment(PowerSegment segmentToAdd,Tile tileToAddSegment)
    {
        

        if(_powerSegments.Count == 0)
        {
            segmentToAdd.CurrentLine = this;
            addSegmentToTile(segmentToAdd, tileToAddSegment, true);
            return 0;
        }
        else
        {
            //Create a list of valid tiles by finding the tiles directly adjacent by 1 tile in the X and Y directions. Then, check if the tileToAdd is a valid tile to add to the power line. 
            /*EXPECTED BUG: It's likely that the current implementation of this system will make it impossible to order the line in the exact way you want if both the beginning and end tile are adjacent to the target tile. 
             Figuring out exactly how to fix this to feel intuitive will be difficult until we have all the systems working properly, however, so we'll cross that bridge when we get to it -T*/

            Tile endTile = _powerSegments[_powerSegments.Count-1].CurrentTile; //The tile at the current end of the power line

            RelativeTiles endRelativeTiles = endTile.getRelativeTiles();
            List<Tile> validTiles = new List<Tile>();
            validTiles.Add(endRelativeTiles.getRelativeTileCoords(-1, 0));
            validTiles.Add(endRelativeTiles.getRelativeTileCoords(1, 0));
            validTiles.Add(endRelativeTiles.getRelativeTileCoords(0, -1));
            validTiles.Add(endRelativeTiles.getRelativeTileCoords(0, 1));


            bool tileIsValid = false;

            foreach (Tile tile in validTiles)
            {
                if(tile == tileToAddSegment)
                {
                    tileIsValid = true;
                }
            }

            if(tileIsValid)
            {
                segmentToAdd.CurrentLine = this;
                addSegmentToTile(segmentToAdd, tileToAddSegment, true);
                return 0;
            }

            //Do the same thing, but check if the tile is valid for the beginning of the line.
            Tile startTile = _powerSegments[0].CurrentTile; //The tile at the beginning of the power line

            RelativeTiles startRelativeTiles = startTile.getRelativeTiles();
            validTiles.Clear();
            validTiles.Add(startRelativeTiles.getRelativeTileCoords(-1, 0));
            validTiles.Add(startRelativeTiles.getRelativeTileCoords(1, 0));
            validTiles.Add(startRelativeTiles.getRelativeTileCoords(0, -1));
            validTiles.Add(startRelativeTiles.getRelativeTileCoords(0, 1));


            tileIsValid = false;

            foreach (Tile tile in validTiles)
            {
                if (tile == tileToAddSegment)
                {
                    tileIsValid = true;
                }
            }

            if (tileIsValid)
            {
                segmentToAdd.CurrentLine = this;
                addSegmentToTile(segmentToAdd, tileToAddSegment, false);
                return 0;
            }
        }

        return -1;
    }

    public int attemptAddSegment(PowerSegment segmentToAdd, Tile tileToAddSegment, lineEnd lineEnd)
    {


        if (_powerSegments.Count == 0)
        {
            segmentToAdd.CurrentLine = this;
            addSegmentToTile(segmentToAdd, tileToAddSegment, true);
            return 0;
        }
        else
        {
            //Create a list of valid tiles by finding the tiles directly adjacent by 1 tile in the X and Y directions. Then, check if the tileToAdd is a valid tile to add to the power line. 

            if(lineEnd == lineEnd.end)
            {
                Tile endTile = _powerSegments[_powerSegments.Count - 1].CurrentTile; //The tile at the current end of the power line

                RelativeTiles endRelativeTiles = endTile.getRelativeTiles();
                List<Tile> validTiles = new List<Tile>();
                validTiles.Add(endRelativeTiles.getRelativeTileCoords(-1, 0));
                validTiles.Add(endRelativeTiles.getRelativeTileCoords(1, 0));
                validTiles.Add(endRelativeTiles.getRelativeTileCoords(0, -1));
                validTiles.Add(endRelativeTiles.getRelativeTileCoords(0, 1));


                bool tileIsValid = false;

                foreach (Tile tile in validTiles)
                {
                    if (tile == tileToAddSegment)
                    {
                        tileIsValid = true;
                    }
                }

                if (tileIsValid)
                {
                    segmentToAdd.CurrentLine = this;
                    addSegmentToTile(segmentToAdd, tileToAddSegment, true);
                    return 0;
                }
            }
            else if(lineEnd == lineEnd.beginning)
            {
                //Do the same thing, but check if the tile is valid for the beginning of the line.
                Tile startTile = _powerSegments[0].CurrentTile; //The tile at the beginning of the power line

                RelativeTiles startRelativeTiles = startTile.getRelativeTiles();
                List<Tile> validTiles = new List<Tile>();
                validTiles.Add(startRelativeTiles.getRelativeTileCoords(-1, 0));
                validTiles.Add(startRelativeTiles.getRelativeTileCoords(1, 0));
                validTiles.Add(startRelativeTiles.getRelativeTileCoords(0, -1));
                validTiles.Add(startRelativeTiles.getRelativeTileCoords(0, 1));


                bool tileIsValid = false;

                foreach (Tile tile in validTiles)
                {
                    if (tile == tileToAddSegment)
                    {
                        tileIsValid = true;
                    }
                }

                if (tileIsValid)
                {
                    segmentToAdd.CurrentLine = this;
                    addSegmentToTile(segmentToAdd, tileToAddSegment, false);
                    return 0;
                }
            }
        }

        return -1;
    }


    private void addSegmentToTile(PowerSegment segmentToAdd, Tile tileToAddSegment, bool insertEnd)
    {
        switch(insertEnd)
        {
            case true:
                _powerSegments.Add(segmentToAdd);
                tileToAddSegment.setPowerObject(segmentToAdd.gameObject);
                break;
            case false:
                _powerSegments.Insert(0, segmentToAdd);
                tileToAddSegment.setPowerObject(segmentToAdd.gameObject);
                break;
        }
    }

    public int clearLine()
    {
        int segmentsCleared = _powerSegments.Count;

        for(int i = 0; i < _powerSegments.Count; i++)
        {
            _powerSegments[i].CurrentTile.clearPowerObject();
        }

        Destroy(gameObject);

        return segmentsCleared;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for(int i = 0; i < _powerSegments.Count - 1; i++)
        {
            Gizmos.DrawLine(_powerSegments[i].transform.position, _powerSegments[i+1].transform.position);
        }
    }

}
