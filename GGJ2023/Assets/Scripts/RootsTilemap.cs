using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RootsTilemap : MonoBehaviour
{
    public const int width = 8;
    public const int height = 16;
    [SerializeField] private Tile currentTile;
    
    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = gameObject.GetComponent<Tilemap>();
        //Debug.Log(tilemap);
        //tilemap.ClearAllTiles(); // make sure the tilemap is empty
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        // get the position of the tile being clicked on
        Vector3 point = GetMouseWorldPosition();
        
        if (point == Vector3.zero) return;

        Vector3Int tilePos = tilemap.WorldToCell(point);

        

        PlaceTile(currentTile, tilePos);
    
    }

    void PlaceTile(Tile tile, Vector3Int pos){
    
        if (!IsTileBuildable(pos)) return;
        
        // place tile
        tilemap.SetTile(pos, tile);
        
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

        return true;
    }

}
