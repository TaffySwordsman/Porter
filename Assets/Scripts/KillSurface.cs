using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSurface : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Player")
			GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().Kill();
	}
}
