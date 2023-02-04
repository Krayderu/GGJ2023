using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private int dragMargin = 10;
    [SerializeField] private float dragSpeed = 10f;
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
        transform.position = position;
    }
}
