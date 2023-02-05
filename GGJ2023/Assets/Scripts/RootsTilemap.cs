using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RootsTilemap : MonoBehaviour
{
    public const int width = 8;
    public const int height = 16;
    [SerializeField] private Texture2D rootTileset;
    [SerializeField] private Tilemap waterTilemap;
    
    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = gameObject.GetComponent<Tilemap>();
        //Debug.Log(tilemap);
        //tilemap.ClearAllTiles(); // make sure the tilemap is empty
    }


    public void PlaceTile(TileData tileData, Vector3Int pos, int rotation){
    
        if (!IsTileBuildable(pos, tileData, rotation)) return;
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
        tile.rotation = rotation;

        // place tile
        tilemap.SetTile(pos, tile);
        // rotate tile
        tilemap.SetTransformMatrix(pos, Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, 90*rotation), Vector3.one));

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

    public bool IsTileBuildable(Vector3Int tilePos, TileData data, int rotation){
        // check if the tile is empty
        if (tilemap.GetTile(tilePos)) return false;
        // don't allow placing tiles above the horizon.
        if (tilePos.y >= 0) return false;
        if (tilePos.y < -height) return false;
        if (tilePos.x >= width/2) return false;
        if (tilePos.x < -width/2) return false;

        // check that the tile can connect to another root tile
        bool canConnect = false;
        
        Vector3Int[] neighbors = new Vector3Int[]
        {
            new Vector3Int(0, 1, 0), // up
            new Vector3Int(1, 0, 0), // right
            new Vector3Int(0, -1, 0), // bottom
            new Vector3Int(-1, 0, 0), // left
        };

        int[] connectionIndices = new int[]
        {
            2, // up -> down
            3, // right -> left
            0, // bottom -> up
            1  // left -> right
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

            int neighborIndex = AddWithWrapAround(i, neighborTile.rotation);
            int selfIndex = AddWithWrapAround(i, rotation);

            if (neighborConnections[connectionIndices[neighborIndex]] && data.edges[selfIndex])
            {
                // The current tile and the neighbor tile can be connected
                canConnect = true;
                break;
            }
        }

        if (!canConnect) return false;

        return true;
    }

    private int AddWithWrapAround(int a, int b)
    {
        return (a + b) % 4;
    }

    public int GetNumberOfRoots(){
        int roots = 0;
        foreach (var position in tilemap.cellBounds.allPositionsWithin) {
            if (tilemap.HasTile(position)) {
                roots++;
            }
        }
        return roots;
    }

    public int GetNumberOfWateredRoots(){
        int wateredRoots = 0;
        foreach (var position in tilemap.cellBounds.allPositionsWithin) {
            if (tilemap.HasTile(position) && waterTilemap.HasTile(position)) {
                wateredRoots++;
            }
        }
        return wateredRoots;
    }

}
