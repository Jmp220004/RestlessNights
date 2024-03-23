using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            addSegmentToTile(segmentToAdd, tileToAddSegment);
            return 0;
        }
        else
        {
            //Create a list of valid tiles by finding the tiles directly adjacent by 1 tile in the X and Y directions. Then, check if the tileToAdd is a valid tile to add to the power line. 
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
                addSegmentToTile(segmentToAdd, tileToAddSegment);
                return 0;
            }
        }

        return -1;
    }

    private void addSegmentToTile(PowerSegment segmentToAdd, Tile tileToAddSegment)
    {
        _powerSegments.Add(segmentToAdd);
        tileToAddSegment.setPowerObject(segmentToAdd.gameObject);
    }


}
