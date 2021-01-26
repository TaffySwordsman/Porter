using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
	public GameObject startCheckpoint;
	public Transform currentCheckpoint;
	public Color inactive;
	public Color active;
	private GameObject player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		SetCheckpoint(startCheckpoint.transform);
		startCheckpoint.GetComponent<Checkpoint>().SetColor(startCheckpoint.transform, active);
	}

	public void SetCheckpoint(Transform checkpoint)
	{
		currentCheckpoint = checkpoint;
	}

	public Transform GetCheckpoint()
	{
		return currentCheckpoint;
	}

	public void RespawnPlayer()
    {
		player.transform.position = currentCheckpoint.position;
		player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
