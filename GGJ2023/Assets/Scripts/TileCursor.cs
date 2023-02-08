using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCursor : MonoBehaviour
{
    [SerializeField] private RootsTilemap rootsTilemap;

    private bool locked = false;
	private Vector3 lastPosition;
    private SpriteRenderer spriteRenderer;
    public int currentRotation = 0;
    private Quaternion startRotation;
    private bool snap = false;


	void Start()
	{
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		lastPosition = transform.position;
        spriteRenderer.sprite = GameManager.Instance.currentTileType.sprite;
        startRotation = transform.rotation;
	}

    // Update is called once per frame
    void Update()
    {
        if (locked) return;

        Vector3 newPos = lastPosition;
        Vector3 mousePos = rootsTilemap.GetMouseWorldPosition();
        Vector3Int tilePos = rootsTilemap.tilemap.WorldToCell(mousePos);

        if (rootsTilemap.IsTileBuildable(tilePos, GameManager.Instance.currentTileType, currentRotation) && GameManager.Instance.CanPay()){
            spriteRenderer.color = new Color(1f,1f,1f,1f);
            snap = true;
        } else {
            spriteRenderer.color = new Color(1f,1f,1f,.5f);
            snap = false;
        }

        // don't show above the horizon
        if (mousePos.y > -1){
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

        // rotate the piece with right click
        if(Input.GetMouseButtonDown(1)){ // right click
            currentRotation = (currentRotation + 1) % 4;
            transform.Rotate(new Vector3(0, 0, 90));
        }

		if (snap) {
            newPos = rootsTilemap.tilemap.CellToWorld(tilePos);
            newPos.x += 1;
            newPos.y += 1;
            newPos.z = rootsTilemap.transform.position.z;

            if (transform.position != newPos)
            {
                transform.position = newPos;
                lastPosition = newPos;
            }
		} else {
            transform.position = mousePos;
            lastPosition = mousePos;
        }

        // ON CLICK if player can pay and can build
        if (!(Input.GetMouseButtonDown(0) && GameManager.Instance.CanPay() && rootsTilemap.IsTileBuildable(tilePos, GameManager.Instance.currentTileType, currentRotation))) return;

        GameManager.Instance.Pay();

        rootsTilemap.PlaceTile(GameManager.Instance.currentTileType, tilePos, currentRotation);

        GameManager.Instance.GetNextTile();
        // update appearance and reset rotation
        spriteRenderer.sprite = GameManager.Instance.currentTileType.sprite;
        currentRotation = 0;
        transform.rotation = startRotation;
    }


	public void Lock(bool value)
	{
		locked = value;
	}
}
