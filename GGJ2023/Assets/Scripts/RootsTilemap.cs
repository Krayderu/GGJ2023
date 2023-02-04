using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RootsTilemap : MonoBehaviour
{
    [SerializeField] private Tile currentTile;
    private Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = gameObject.GetComponent<Tilemap>();
        Debug.Log(tilemap);
        tilemap.ClearAllTiles(); // make sure the tilemap is empty
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        // get the position of the tile being clicked on
        Vector3 point = GetMouseWorldPosition();
        
        if (point == Vector3.zero) return;

        Vector3Int tilePos = tilemap.WorldToCell(point);

        // don't allow placing tiles above the horizon.
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
