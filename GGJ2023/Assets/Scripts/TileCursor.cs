using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCursor : MonoBehaviour
{
    [SerializeField] private RootsTilemap rootsTilemap;


    private bool locked = false;
	private Vector3 lastPosition;


	void Start()
	{
		lastPosition = transform.position;
	}

    // Update is called once per frame
    void Update()
    {

        Vector3 newPos = lastPosition;

		if (!locked)
		{
			Vector3 point = rootsTilemap.GetMouseWorldPosition();
			Vector3Int tilePos = rootsTilemap.tilemap.WorldToCell(point);

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
    }


	public void LockPosition(bool value)
	{
		locked = value;
	}
}
