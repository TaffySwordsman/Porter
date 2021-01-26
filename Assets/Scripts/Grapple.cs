using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
	////GrapplePlayer
	//public AnimationCurve forceCurve;
	//private Transform CamParent;
	//private ac_CharacterController Player;
	//private GameManager GM;
	//private Rigidbody PlayerPhysics;
	//private GrapplePoint Grapple;
	//private Vector3 PreviousPosition;
	//private float Distance;
	//private float DistanceModifier;
	//private bool movingAway;
	//private Transform Direction;

	//private void Start()
	//{
	//	this.CamParent = GameObject.FindGameObjectWithTag("MainCamera").transform;
	//	this.Player = GameObject.FindGameObjectWithTag("Player").GetComponent<ac_CharacterController>();
	//	this.GM = this.Player.GM;
	//	this.PlayerPhysics = this.GetComponent<Rigidbody>();
	//	if ((double)this.PlayerPhysics.velocity.y <= -2.5)
	//		this.PlayerPhysics.velocity = new Vector3(this.PlayerPhysics.velocity.x, 0.0f, this.PlayerPhysics.velocity.z);
	//	this.Direction = new GameObject("LookDirection").transform;
	//	this.Direction.transform.position = this.transform.position;
	//	this.Direction.LookAt(this.Grapple.transform.position);
	//	this.PlayerPhysics.AddForce(this.Direction.transform.forward * 750f);
	//	this.PlayerPhysics.AddForce(this.transform.up * 350f);
	//	Object.Destroy((Object)this.Direction.gameObject);
	//}

	//private void Update()
	//{
	//	if (KeyBindingManager.GetKey(KeyAction.Forward) || (double)Input.GetAxis("Vertical") < 0.0)
	//		this.PlayerPhysics.AddForce(this.CamParent.transform.forward * 3f);
	//	if (this.movingAway)
	//	{
	//		this.Grapple.ResetConnection();
	//		this.PlayerPhysics.useGravity = true;
	//	}
	//	if (!KeyBindingManager.GetKeyDown(KeyAction.Jump) && !KeyBindingManager.GetKeyDown(KeyAction.Equipment) && (!Input.GetButtonDown("A") && (double)Input.GetAxis("LeftTrigger") < 0.5))
	//		return;
	//	this.Grapple.DisconnectGrapple();
	//	Object.Destroy((Object)this.gameObject);
	//}

	//private void FixedUpdate()
	//{
	//	this.Distance = Vector3.Distance(this.transform.position, this.Grapple.transform.position);
	//	if ((double)this.Distance <= 2.0)
	//	{
	//		this.Grapple.DisconnectGrapple();
	//		Object.Destroy((Object)this.gameObject);
	//	}
	//	this.movingAway = (double)Vector3.Distance(this.PreviousPosition, this.Grapple.transform.position) < (double)Vector3.Distance(this.transform.position, this.Grapple.transform.position);
	//	this.PreviousPosition = this.transform.position;
	//}

	//public void AssignGrapplePoint(GrapplePoint Point)
	//{
	//	this.Grapple = Point;
	//}

	////GrappleTrail
	//private Transform ReturnPoint;
	//private LineRenderer LR;

	//private void Start()
	//{
	//	this.LR = this.GetComponent<LineRenderer>();
	//}

	//private void Update()
	//{
	//	this.LR.SetPosition(0, this.transform.position);
	//	this.LR.SetPosition(1, this.ReturnPoint.transform.position);
	//}

	////GrapplePoint
	//public GameObject Hook;
	//private bool IsConnected;
	//private ac_CharacterController Player;
	//private Transform RopePoint;
	//private FixedJoint CurrentGrapplePoint;
	//private LineRenderer LR;
	//private bool Pulling;

	//private void Start()
	//{
	//	this.Player = GameObject.FindGameObjectWithTag("Player").GetComponent<ac_CharacterController>();
	//}

	//public void GrapplePlayer()
	//{
	//	this.CurrentGrapplePoint = Object.Instantiate<GameObject>(this.Hook, this.transform.position, this.transform.rotation).GetComponent<FixedJoint>();
	//	this.LR = this.CurrentGrapplePoint.GetComponent<LineRenderer>();
	//	this.IsConnected = true;
	//}

	//public void DisconnectGrapple()
	//{
	//	this.IsConnected = false;
	//	this.LR.SetPosition(0, this.transform.position);
	//	this.LR.SetPosition(1, this.transform.position);
	//}

	//private void Update()
	//{
	//	if (!this.IsConnected)
	//		return;
	//	this.LR.SetPosition(0, this.CurrentGrapplePoint.transform.position);
	//	this.LR.SetPosition(1, this.RopePoint.transform.position);
	//}

	//public void HoldConnection()
	//{
	//	if (this.Pulling)
	//		return;
	//	this.CurrentGrapplePoint.connectedBody = (Rigidbody)null;
	//	this.Pulling = true;
	//}

	//public void ResetConnection()
	//{
	//	this.Pulling = false;
	//}
}
