using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RootsTilemap : MonoBehaviour
{
    [SerializeField] private int width = 14;
    [SerializeField] private int height = 64;
    [SerializeField] private Texture2D rootTileset;
    [SerializeField] private Tilemap waterTilemap;
    
    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = gameObject.GetComponent<Tilemap>();
        Debug.Log(GetNumberOfRoots());
    }

    public void PlaceTile(TileData tileData, Vector3Int pos, int rotation){
        if (!IsTileBuildable(pos, tileData, rotation)) return;

        var path = rootTileset.name;
        Sprite[] tileSprites = Resources.LoadAll<Sprite>(path);
        Sprite matchingSprite = tileSprites[0];

		// find the sprite
		foreach (Sprite sprite in tileSprites){
			if (sprite.name == tileData.sprite.name){
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
    }

    public Vector3 GetMouseWorldPosition(){
        // FIXME: I should be in TileCursor.cs
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
        
        //if(!GameManager.Instance.CanPay()) return false;

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

        for (int i = 0; i < neighbors.Length; i++){
            Vector3Int neighborPos = tilePos + neighbors[i];
            RootTile neighborTile = tilemap.GetTile<RootTile>(neighborPos);

            if (neighborTile == null)
            {
                continue;
            }

            bool[] neighborConnections = neighborTile.data.edges;

            // that condition was not easy to find.
            int neighborIndex = (i + neighborTile.rotation) % 4;
            int selfIndex = (i + rotation) % 4;
            if (neighborConnections[connectionIndices[neighborIndex]] && data.edges[selfIndex]){
                // The current tile and the neighbor tile can be connected
                canConnect = true;
                break;
            }
        }

        if (!canConnect) return false;

        return true;
    }

    public int GetNumberOfRoots(){
        int roots = 0;
        foreach (var position in tilemap.cellBounds.allPositionsWithin) {
            if (tilemap.GetTile<RootTile>(position)) {
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
