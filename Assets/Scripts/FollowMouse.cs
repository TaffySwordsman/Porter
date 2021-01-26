using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

	private Vector3 mousePos;
	private Vector3 targetVector;
	float moveSpeed = 0.01f;
	private Vector3 mouseRef = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
		Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
		targetVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
		transform.position = Vector3.SmoothDamp(transform.position, targetVector, ref mouseRef, moveSpeed);
	}
}
