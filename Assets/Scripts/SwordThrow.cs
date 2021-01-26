using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordThrow : MonoBehaviour
{
	public LayerMask teleportLayers;
	public float speed = 20f;
	public int damage = 2;
	public float lifeTime = 2f;
	private Rigidbody2D rb;
	private SpriteRenderer sword;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * speed;
		sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<SpriteRenderer>();
	}

	private void Awake()
	{
		Destroy(gameObject, lifeTime);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.layer == 8)
		{
			Destroy(gameObject, 0.1f);
		}

		if (collision.gameObject.layer == 31)
		{
			GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
			GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().Teleport(transform.position);
			EZCameraShake.CameraShaker.Instance.ShakeOnce(10f, 20f, 0f, .5f);
			Destroy(gameObject);
		}
	}

	private void OnDestroy()
	{
		sword.enabled = true;
	}
}
