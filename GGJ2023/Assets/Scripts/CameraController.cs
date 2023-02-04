using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private int dragMargin = 10;
    [SerializeField] private float dragSpeed = 10f;
    [SerializeField] private int maxHeight = 500;
    [SerializeField] private int minHeight = -500;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 position = transform.position;
        Vector3 currentPanSpeed = Vector3.zero;

        // Get mouse input and apply movement
		if (Input.mousePosition.y >= Screen.height - dragMargin)
		{
			currentPanSpeed.y = dragSpeed;
		}
		if (Input.mousePosition.y <= dragMargin)
		{
            currentPanSpeed.y = -dragSpeed;
		}

        position.y += currentPanSpeed.y * Time.deltaTime;

        // Limit camera movement
		//position.x = Mathf.Clamp(position.x, -panLimit.x, panLimit.x);
		position.y = Mathf.Clamp(position.y, minHeight, maxHeight);

        transform.position = position;
    }
}
