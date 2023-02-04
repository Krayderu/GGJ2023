using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCursor : MonoBehaviour
{
    [SerializeField] private RootsTilemap rootsTilemap;


    private bool locked = false;
	private Vector3 lastPosition;
    private TileData currentTile;


	void Start()
	{
		lastPosition = transform.position;
        currentTile = GameManager.Instance.GetNextTile();
        Debug.Log(currentTile);
	}

    // Update is called once per frame
    void Update()
    {

        Vector3 newPos = lastPosition;
        Vector3 point = rootsTilemap.GetMouseWorldPosition();
        if (point == Vector3.zero) return;
        Vector3Int tilePos = rootsTilemap.tilemap.WorldToCell(point);

		if (!locked)
		{
            if (!rootsTilemap.IsTileBuildable(tilePos)) return;

            newPos = rootsTilemap.tilemap.CellToWorld(tilePos);
            newPos.x += 4;
            newPos.y += 4;
            newPos.z = rootsTilemap.transform.position.z;
		}

		if (transform.position != newPos)
		{
			transform.position = newPos;
			lastPosition = newPos;
		}

        // ON CLICK
        if (!Input.GetMouseButtonDown(0)) return;

        rootsTilemap.PlaceTile(currentTile, tilePos);

        currentTile = GameManager.Instance.GetNextTile();
    
    }


	public void LockPosition(bool value)
	{
		locked = value;
	}
}
