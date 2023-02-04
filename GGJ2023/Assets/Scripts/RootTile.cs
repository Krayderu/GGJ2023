using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New RootTile Type", menuName = "Tiles/RootTile Type")]
public class RootTile : Tile
{
    public TileData data;
    public int rotation;
}
