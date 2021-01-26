using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDisableObject : MonoBehaviour
{
	public GameObject disable;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		gameObject.SetActive(false);
		disable.SetActive(false);
	}
}
