using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RelativeTiles
{
    public Tile TopL;
    public Tile TopM;
    public Tile TopR;
    public Tile MidL;
    public Tile thisTile;
    public Tile MidR;
    public Tile BotL;
    public Tile BotM;
    public Tile BotR;

    /// <summary>
    /// Returns the tile relative to this tile in a 3x3 grid where the center tile is the current tile at coordinate (0,0). Returns null if the value is outside of the relative range.
    /// </summary>
    /// <param name="relativeX">The relative X value compared to the current tile.</param>
    /// <param name="relativeY">The relative Y value compared to the current tile.</param>
    /// <returns></returns>
    public Tile getRelativeTileCoords(int relativeX, int relativeY)
    {
        switch(relativeY)
        {
            case -1:
                switch(relativeX)
                {
                    case -1:
                        return BotL;
                    case 0:
                        return BotM;
                    case 1:
                        return BotR;
                }
                return null;
            case 0:
                switch (relativeX)
                {
                    case -1:
                        return MidL;
                    case 0:
                        return thisTile;
                    case 1:
                        return MidR;
                }
                return null;
            case 1:
                switch (relativeX)
                {
                    case -1:
                        return TopL;
                    case 0:
                        return TopM;
                    case 1:
                        return TopR;
                }
                return null;
        }

        //Returns null if the relative X value and Y value are not properly within the relative tile storage bounds
        return null;
    }
}
