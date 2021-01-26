using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public string fireButton;
	public Transform firePoint;
	public GameObject projectile;
	public Component script;
	public Transform player;
	private Movement playerMovement;
	private Collision playerColl;	

	void Update()
	{
		if (Input.GetButtonDown(fireButton))

			switch (script.GetType().Name)
			{
				case "Bullet":
					FireBullet(projectile);
						break;

				case "GrapplingHook":
					HookShot(projectile);
					break;

				case "SwordThrow":
					Teleport(projectile);
						break;
			}
	}

	void FireBullet(GameObject bullet)
	{
		player = transform.parent.parent;
		playerMovement = player.GetComponent<Movement>();
		playerColl = player.GetComponent<Collision>();

		Instantiate(bullet, firePoint.position, firePoint.rotation);
		EZCameraShake.CameraShaker.Instance.ShakeOnce(5f, 10f, .05f, .05f);
		playerMovement.Recoil(firePoint.rotation, ((Bullet)script).recoil);
	}

	void Teleport(GameObject sword)
	{
		GetComponent<SpriteRenderer>().enabled = false;
		Instantiate(sword, firePoint.position, firePoint.rotation);
	}

	void HookShot(GameObject hook)
	{
		Instantiate(hook, firePoint.position, firePoint.rotation);
	}
}
