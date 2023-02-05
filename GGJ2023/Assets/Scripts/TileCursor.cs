using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCursor : MonoBehaviour
{
    [SerializeField] private RootsTilemap rootsTilemap;


    private bool locked = false;
	private Vector3 lastPosition;
    private TileData currentTile;
    private SpriteRenderer spriteRenderer;
    public int currentRotation = 0;
    private Quaternion startRotation;


	void Start()
	{
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Debug.Log(spriteRenderer);
		lastPosition = transform.position;
        currentTile = GameManager.Instance.GetNextTile();
        spriteRenderer.sprite = currentTile.sprite;
        startRotation = transform.rotation;
        //Debug.Log(currentTile);
	}

    // Update is called once per frame
    void Update()
    {

        Vector3 newPos = lastPosition;
        Vector3 point = rootsTilemap.GetMouseWorldPosition();
        Vector3Int tilePos = rootsTilemap.tilemap.WorldToCell(point);

        if (rootsTilemap.IsTileBuildable(tilePos, currentTile, currentRotation)){
            //spriteRenderer.enabled = true;
            spriteRenderer.color = new Color(1f,1f,1f,1f);
        } else {
            //spriteRenderer.enabled = false;
            spriteRenderer.color = new Color(1f,1f,1f,.5f);
        }

        // detect scroll
        // Vector2 vec = Input.Mouse.current.scroll.ReadValue();
        // var scroll = vec.y;
        // if(Input.GetAxis("Mouse ScrollWheel") > 0)
        // {
        //     //wheel goes up
        //     currentRotation = (currentRotation-1) % 4;
        //     transform.Rotate(new Vector3(0, 0, -90));
        // }
        // else if(Input.GetAxis("Mouse ScrollWheel") < 0)
        // {
        //     //wheel goes down
        //     currentRotation = (currentRotation+1) % 4;
        //     transform.Rotate(new Vector3(0, 0, 90));
        // }

        if(Input.GetKeyDown(KeyCode.R)){
            currentRotation++;
            if (currentRotation > 3){
                currentRotation = 0;
            }
            transform.Rotate(new Vector3(0, 0, 90));
        }

		if (!locked)
		{
            if (!rootsTilemap.IsTileBuildable(tilePos, currentTile, currentRotation)) return;

            newPos = rootsTilemap.tilemap.CellToWorld(tilePos);
            newPos.x += 2;
            newPos.y += 2;
            newPos.z = rootsTilemap.transform.position.z;
		}

		if (transform.position != newPos)
		{
			transform.position = newPos;
			lastPosition = newPos;
		}

        // ON CLICK
        if (!Input.GetMouseButtonDown(0)) return;

        GameManager.Instance.Pay();

        rootsTilemap.PlaceTile(currentTile, tilePos, currentRotation);

        currentTile = GameManager.Instance.GetNextTile();
        spriteRenderer.sprite = currentTile.sprite;
        currentRotation = 0;
        transform.rotation = startRotation;
    
    }


	public void LockPosition(bool value)
	{
		locked = value;
	}
}
