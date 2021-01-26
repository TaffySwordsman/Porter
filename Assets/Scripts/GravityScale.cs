using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScale : MonoBehaviour
{
	public Rigidbody2D rb;
	public Movement controller;
	public Collision coll;

	[Range(0f, 1f)]
	public float squishPercent = 1f;
	[Range(1f, 2f)]
	public float stretchPercent = 1f;
	[Range(0f, 1f)]
	public float landCompression;
	[Range(0f, 1f)]
	public float crouchPercent = 1f;
	public float maxLandVelocity = 40f;

	public float jumpSmooth = .2f;
	public float fallSmooth = .5f;

	private Vector3 defScale;
	private Vector3 upScale;
	private Vector3 downScale;
	private Vector3 landScale;
	private Vector3 crouchScale;
	private Vector3 scaleVel = Vector3.zero;

	private float yVel;
	private float oldVel = 0f;
	private float landForce;

	bool oldGround;

	private void Start()
	{
		defScale = transform.localScale;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		yVel = rb.velocity.y;
		upScale = new Vector3(defScale.x, defScale.y * squishPercent, 1);
		downScale = new Vector3(defScale.x * squishPercent * .9f, defScale.y * stretchPercent, 1);
		crouchScale = new Vector3(defScale.x, defScale.y * crouchPercent, 1);

		//Squish when jumping up
		if (yVel > 2 && !controller.wallSlide)
			transform.localScale = Vector3.SmoothDamp(transform.localScale, upScale, ref scaleVel, jumpSmooth);

		//Stretch when falling down
		else if (yVel < -8 && !controller.wallSlide)
			transform.localScale = Vector3.SmoothDamp(transform.localScale, downScale, ref scaleVel, fallSmooth);
		
		//Squish when landing
		else if (coll.onGround && !oldGround)
		{
			landForce = (Mathf.Clamp(-oldVel, 0f, maxLandVelocity) / maxLandVelocity);
			//Set landForce to max if it updated a frame too late
			if (landForce == 0)
				landForce = 1;

			landScale = new Vector3(defScale.x, defScale.y - defScale.y * (landCompression * landForce), 1);
			transform.localScale = Vector3.SmoothDamp(transform.localScale, landScale, ref scaleVel, 0f);
		}

		//Squish if crouching
		else if (controller.crouching && coll.onGround)
		{
			if (coll.onGround)
				transform.localScale = Vector3.SmoothDamp(transform.localScale, crouchScale, ref scaleVel, .05f);
		}

		//Else return to normal
		else if (!controller.crouching)
			transform.localScale = Vector3.SmoothDamp(transform.localScale, defScale, ref scaleVel, .1f);

		oldGround = coll.onGround;
		oldVel = yVel;

	}
}
