using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
	private float flipX;
	public bool faceRight;

	// Update is called once per frame
	void Update()
    {
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 1f;

		Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;

		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		//transform.rotation = Quaternion.Euler(0, 0, angle);

		flipX = faceRight ? 0 : 180;
		angle = faceRight ? angle : -angle;
		transform.rotation = Quaternion.Euler(flipX, 0, angle);

		//Flip based on side
		if (mousePos.x > 0)
		{
			//GetComponent<SpriteRenderer>().flipY = false;
			faceRight = true;
		}
		else if (mousePos.x < 0)
		{
			//GetComponent<SpriteRenderer>().flipY = true;
			faceRight = false;
		}
	}
}
