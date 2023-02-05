using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private int dragMargin = 10;
    [SerializeField] private float dragSpeed = 10f;
    [SerializeField] private int maxHeight = 500;
    [SerializeField] private int minHeight = -500;
    
    private bool lerping = false;
    private float destinationY;



    // Update is called once per frame
    void Update()
    {
		Vector3 position = transform.position;
        Vector3 currentPanSpeed = Vector3.zero;

        if (lerping) {
            position.y = Mathf.Lerp(transform.position.y, destinationY, Time.deltaTime);

            if (Mathf.Abs(transform.position.y - destinationY) <= 1){
                lerping = false;
            }
        } else {
            // Get mouse input and apply movement
            if (Input.mousePosition.y >= Screen.height - dragMargin)
            {
                currentPanSpeed.y = dragSpeed;
            }
            if (Input.mousePosition.y <= dragMargin)
            {
                currentPanSpeed.y = -dragSpeed;
            }
        }

        

        position.y += currentPanSpeed.y * Time.deltaTime;

        // Limit camera movement
		//position.x = Mathf.Clamp(position.x, -panLimit.x, panLimit.x);
		position.y = Mathf.Clamp(position.y, minHeight, maxHeight);

        transform.position = position;
    }

    public void moveToY(float y){
        destinationY = y;
        lerping = true;
    }
}
