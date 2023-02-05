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
    private bool snap = false;


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
            snap = true;
        } else {
            //spriteRenderer.enabled = false;
            spriteRenderer.color = new Color(1f,1f,1f,.5f);
            snap = false;
        }

        if (point.y > 0){
            spriteRenderer.enabled = false;
        } else {
            spriteRenderer.enabled = true;
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

        if(Input.GetMouseButtonDown(1)){ // right click
            currentRotation++;
            if (currentRotation > 3){
                currentRotation = 0;
            }
            transform.Rotate(new Vector3(0, 0, 90));
        }

		if (snap && !locked)
		{
            newPos = rootsTilemap.tilemap.CellToWorld(tilePos);
            newPos.x += 2;
            newPos.y += 2;
            newPos.z = rootsTilemap.transform.position.z;

            if (transform.position != newPos)
            {
                transform.position = newPos;
                lastPosition = newPos;
            }
		}

        if (!snap && !locked){
            var mousePos = rootsTilemap.GetMouseWorldPosition();
            transform.position = mousePos;
            lastPosition = mousePos;
        }

        // ON CLICK
        if (!Input.GetMouseButtonDown(0) || !GameManager.Instance.CanPay()) return;

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
