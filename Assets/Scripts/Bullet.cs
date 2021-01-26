using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed = 20f;
	public int damage = 2;
	public float recoil = 5f;
	public float lifeTime = 2f;
	private Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * speed;
	}

	private void Awake()
	{
		Destroy(gameObject, lifeTime);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}

	////BulletScript
	//public bool PlayerBullet = true;
	//[Header("Bullet Stats")]
	//public float damage = 16f;
	//public float speed = 1000f;
	//public float length = 3.5f;
	//public int DamageChance = 1;
	//[Header("Bullet Spawns")]
	//public string hitEffectTag = "HitBox";
	//public string InteractEffectTag = "Object";
	//public bool SlowBullet = true;
	//[HideInInspector]
	//public float BulletAssist = 0.1f;
	//public bool HasBalistics;
	//public LayerMask PotentialLayers;
	//public GameObject hitEffect;
	//public GameObject missEffect;
	//[Header("Bullet Abilities")]
	//public bool ShouldParent;
	//public bool DamageFalloff;
	//[ConditionalField("DamageFalloff", null)]
	//public float EffectiveRange;
	//public bool TimedDetonation;
	//[ConditionalField("TimedDetonation", null)]
	//public float TimeToDetonate;
	//private Transform Target;
	//private RaycastHit hit;
	//private Vector3 StartingPosition;
	//private SphereCollider AimAssist;
	//private Rigidbody MyPhysics;
	//private float Distance;
	//private float StableSpeed;
	//private bool HasHit;
	//private bool Frozen;
	//private float SpeedSave;

	//private void Awake()
	//{
	//	this.StartingPosition = this.transform.position;
	//	if (Physics.Raycast(this.transform.position, this.transform.forward, out this.hit, this.length, this.PotentialLayers.value))
	//		this.Collide();
	//	this.StableSpeed = this.speed;
	//	if (this.PlayerBullet)
	//	{
	//		this.AimAssist = this.gameObject.AddComponent<SphereCollider>();
	//		this.AimAssist.isTrigger = true;
	//		this.AimAssist.radius = this.BulletAssist;
	//	}
	//	if (this.TimedDetonation)
	//		this.StartCoroutine(this.Detonate());
	//	if (!this.HasBalistics)
	//		return;
	//	this.MyPhysics = this.GetComponent<Rigidbody>();
	//	this.MyPhysics.isKinematic = false;
	//}

	//private void Update()
	//{
	//	if (this.Frozen)
	//	{
	//		if ((double)this.speed > 0.0)
	//			this.speed -= Time.deltaTime * 85f;
	//		else
	//			this.speed = 0.0f;
	//	}
	//	this.transform.Translate(Vector3.forward * Time.deltaTime * this.speed);
	//	if (Physics.Raycast(this.transform.position, this.transform.forward, out this.hit, this.length, this.PotentialLayers.value))
	//		this.Collide();
	//	if (this.DamageFalloff && !this.Frozen)
	//	{
	//		if ((double)Vector3.Distance(this.StartingPosition, this.transform.position) >= (double)this.EffectiveRange)
	//			this.damage -= Time.deltaTime * 10f;
	//		if ((double)this.damage <= 1.0)
	//			Object.Destroy((Object)this.gameObject);
	//	}
	//	if (this.SlowBullet)
	//		this.speed = this.StableSpeed * Time.timeScale;
	//	if (!this.PlayerBullet)
	//		return;
	//	this.AimAssist.radius = this.BulletAssist;
	//}

	//private void Collide()
	//{
	//	if (this.Frozen)
	//		return;
	//	if (this.hit.transform.tag == this.hitEffectTag)
	//	{
	//		this.hit.collider.SendMessage("Damage", (object)this.damage, SendMessageOptions.DontRequireReceiver);
	//		GameObject gameObject = Object.Instantiate<GameObject>(this.hitEffect, this.hit.point, this.transform.rotation);
	//		if (this.ShouldParent)
	//			gameObject.transform.parent = this.hit.collider.transform;
	//		--this.DamageChance;
	//		if (this.DamageChance > 0)
	//			return;
	//		Object.Destroy((Object)this.gameObject);
	//	}
	//	else
	//	{
	//		Object.Instantiate<GameObject>(this.missEffect, this.hit.point + this.hit.normal * 0.01f, Quaternion.LookRotation(this.hit.normal));
	//		if (this.hit.transform.tag == this.InteractEffectTag || this.hit.transform.tag == "Interactable")
	//			this.hit.collider.SendMessage("Interact");
	//		Object.Destroy((Object)this.gameObject);
	//	}
	//}

	//private void OnTriggerEnter(Collider other)
	//{
	//	if (this.Frozen || (!(other.tag == "Enemy") || !this.PlayerBullet) && (!(other.tag == "HitBox") || !this.PlayerBullet))
	//		return;
	//	if (!this.HasHit)
	//	{
	//		other.SendMessage("Damage", (object)this.damage, SendMessageOptions.DontRequireReceiver);
	//		GameObject gameObject = Object.Instantiate<GameObject>(this.hitEffect, other.transform.position, this.transform.rotation);
	//		if (this.ShouldParent)
	//			gameObject.transform.parent = other.gameObject.transform;
	//		--this.DamageChance;
	//		if (this.DamageChance > 0)
	//			return;
	//		this.HasHit = true;
	//		Object.Destroy((Object)this.gameObject);
	//	}
	//	else
	//		Object.Destroy((Object)this.gameObject);
	//}

	//private IEnumerator Detonate()
	//{
	//	BulletScript bulletScript = this;
	//	yield return (object)new WaitForSeconds(bulletScript.TimeToDetonate);
	//	if (!bulletScript.Frozen)
	//	{
	//		Object.Instantiate<GameObject>(bulletScript.missEffect, bulletScript.transform.position, bulletScript.transform.rotation);
	//		Object.Destroy((Object)bulletScript.gameObject);
	//	}
	//}

	//public void Freeze()
	//{
	//	this.SpeedSave = this.speed;
	//	this.speed = 15f;
	//	if (this.HasBalistics)
	//		this.MyPhysics.isKinematic = true;
	//	this.Frozen = true;
	//}

	//public void UnFreeze()
	//{
	//	this.speed = this.SpeedSave;
	//	if (this.HasBalistics)
	//		this.MyPhysics.isKinematic = false;
	//	this.Frozen = false;
	//}

}
