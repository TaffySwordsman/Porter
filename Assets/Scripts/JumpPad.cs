using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
	public float UpForce;
	public float ForwardForce;
	private Rigidbody Player;
	private bool HasLaunched;

	//private void Start()
	//{
	//	this.Player = GameManager.GM.Player.GetComponent<Rigidbody>();
	//}

	//private void Update()
	//{
	//	if ((double)Vector3.Distance(this.transform.position, this.Player.transform.position) > 2.0 || this.HasLaunched)
	//		return;
	//	this.HasLaunched = true;
	//	this.Launch();
	//	this.StartCoroutine(this.LaunchCooldown());
	//}

	//public void Launch()
	//{
	//	this.Player.transform.position = this.transform.position;
	//	this.Player.gameObject.GetComponent<ac_CharacterController>().StopDashing();
	//	this.Player.gameObject.GetComponent<ac_CharacterController>().Unground();
	//	this.Player.gameObject.GetComponent<ac_CharacterController>().PlayerUnCrouch();
	//	this.Player.velocity = Vector3.zero;
	//	this.Player.velocity = this.transform.up * this.UpForce + this.transform.forward * this.ForwardForce;
	//	this.Player.gameObject.GetComponent<ac_CharacterController>().MovementShake();
	//	this.gameObject.GetComponent<AudioSource>().Play();
	//}

	//private IEnumerator LaunchCooldown()
	//{
	//	yield return (object)new WaitForSeconds(1f);
	//	this.HasLaunched = false;
	//}
}
