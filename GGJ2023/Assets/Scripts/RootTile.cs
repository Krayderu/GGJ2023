using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New RootTile Type", menuName = "Tiles/RootTile Type")]
public class RootTile : Tile
{
    public TileData data;
    public int rotation;
    //public Sprite sprite;

    // public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    // {
    //     // This method is called when the tile is refreshed, for example, when the surrounding tiles are changed
    // }

    // public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    // {
    //     // Add the RootTile component to the GameObject
    //     RootTile rootTile = go.AddComponent<RootTile>();

    //     // Assign the TileData asset to the RootTile component
    //     rootTile.tileData = tileData;

    //     return true;
    // }

    // public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
    // {
    //     tileData.sprite = sprite;
    // }
}
