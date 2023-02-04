using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RootsTilemap : MonoBehaviour
{
    public Tile currentTile;
    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject);
        //tilemap.ClearAllTiles();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        // get the position of the tile being clicked on
        Vector3 point = GetMouseWorldPosition();
        //Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (point == Vector3.zero) return;
        Debug.Log(point);
        Vector3Int tilePos = tilemap.WorldToCell(point);

        Debug.Log("PLACING TILE" + tilePos);
        if (tilePos.y >= 0) return;

        PlaceTile(currentTile, tilePos);
        
    }

    void PlaceTile(Tile tile, Vector3Int pos){
        // check if the tile is empty
        if (!tilemap.GetTile(pos))
        {
            // place tile
            tilemap.SetTile(pos, tile);
        }
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

}
