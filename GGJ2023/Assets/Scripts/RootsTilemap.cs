using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RootsTilemap : MonoBehaviour
{
    public const int width = 8;
    public const int height = 16;
    [SerializeField] private Texture2D rootTileset;
    
    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = gameObject.GetComponent<Tilemap>();
        //Debug.Log(tilemap);
        //tilemap.ClearAllTiles(); // make sure the tilemap is empty
    }


    public void PlaceTile(TileData tileData, Vector3Int pos, Quaternion rotation){
    
        if (!IsTileBuildable(pos)) return;
        var path = rootTileset.name;
        Debug.Log(path);
        Sprite[] tileSprites = Resources.LoadAll<Sprite>(path);

        Sprite matchingSprite = tileSprites[0];

		// find the sprite
		foreach (Sprite sprite in tileSprites)
		{
			if (sprite.name == tileData.sprite.name)
			{
				matchingSprite = sprite;
				break;
			}
		}

        RootTile tile = ScriptableObject.CreateInstance<RootTile>();
		tile.sprite = matchingSprite;
        tile.data = tileData;

        // place tile
        tilemap.SetTile(pos, tile);

        tilemap.SetTransformMatrix(pos, Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one));

        // Add the RootTile component to the TileBase instance
        //RootTile rootTile = tile.gameObject.AddComponent<RootTile>();

        // Assign the TileData asset to the RootTile component
        //rootTile.tileData = tileData;


    }

    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Camera.main.transform.forward, transform.position);
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            return hitPoint;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public bool IsTileBuildable(Vector3Int tilePos){
        // check if the tile is empty
        if (tilemap.GetTile(tilePos)) return false;
        // don't allow placing tiles above the horizon.
        if (tilePos.y >= 0) return false;
        if (tilePos.y < -height) return false;
        if (tilePos.x >= width/2) return false;
        if (tilePos.x < -width/2) return false;

        // check that the tile can connect to another root tile
        // TODO
        bool canConnect = false;
        
        Vector3Int[] neighbors = new Vector3Int[]
        {
            new Vector3Int(0, 1, 0),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(1, 0, 0),
        };

        int[] connectionIndices = new int[]
        {
            2,
            1,
            0,
            3
        };

        for (int i = 0; i < neighbors.Length; i++)
        {
            Vector3Int neighborPos = tilePos + neighbors[i];
            RootTile neighborTile = tilemap.GetTile<RootTile>(neighborPos);
            if (neighborTile == null)
            {
                continue;
            }

            bool[] neighborConnections = neighborTile.data.edges;
            if (neighborConnections[connectionIndices[i]])
            {
                // The current tile and the neighbor tile can be connected
                canConnect = true;
                break;
            }
        }

        //if (!canConnect) return false;

        // foreach (Vector3Int offset in neighbors)
        // {
        //     Vector3Int neighborPos = currentTilePos + offset;
        //     TileBase neighborTile = tilemap.GetTile(neighborPos);
        //     if (neighborTile != null)
        //     {
        //         // Do something with the neighboring tile
        //     }
        // }

        return true;
    }

}
