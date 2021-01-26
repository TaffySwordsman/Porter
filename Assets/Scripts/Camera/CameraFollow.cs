using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public Camera mainCamera;
	public float maxDistance = 0.4f;
	[Range (0, .3f)]
	public float camSmooth = 0.1f;

	Vector3 mousePos;
	Vector3 position;
	Vector3 destination;
	Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
		mousePos = Input.mousePosition * maxDistance + new Vector3(Screen.width, Screen.height, 0f) * ((1f - maxDistance) * 0.5f);
		position = (target.position + mainCamera.ScreenToWorldPoint(mousePos)) / 2f;
		destination = new Vector3(position.x, position.y, -10);
		transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, camSmooth);
	}
}
