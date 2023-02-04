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


    public void PlaceTile(TileData tileData, Vector3Int pos){
    
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

        Tile tile = ScriptableObject.CreateInstance<Tile>();
		tile.sprite = matchingSprite;

                // place tile
        tilemap.SetTile(pos, tile);

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
        // Idea: each time we place a root, keep in memory the tiles that can connect and the direction of the connection

        return true;
    }

}
