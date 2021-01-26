using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	private Respawn respawn;

	private void Start()
	{
		respawn = GameObject.FindGameObjectWithTag("GameController").GetComponent<Respawn>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		SetColor(respawn.GetCheckpoint(), respawn.inactive);
		respawn.SetCheckpoint(transform);
		SetColor(transform, respawn.active);
	}

	public void SetColor(Transform cp, Color color)
	{
		cp.GetComponent<SpriteRenderer>().color = color;
	}
}
