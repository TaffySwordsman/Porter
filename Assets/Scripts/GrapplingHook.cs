using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
	public float speed = 20f;
	public float distance = 2f;
	private Rigidbody2D rb;
	private GameObject player;

	private void Start()
	{
		player = GameObject.FindWithTag("Player");
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * speed;
	}

	private void Awake()
	{
		Destroy(gameObject, distance / speed);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}
}
