using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	public GameObject[] weapons;
	public int activeWeap = 0;

    // Start is called before the first frame update
    void Start()
    {
		SetActive(activeWeap);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Alpha1) && activeWeap != 0)
			SetActive(0);
		if (Input.GetKeyDown(KeyCode.Alpha2) && activeWeap != 1)
			SetActive(1);

		//Scroll wheel weapon switching
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
			NextWeap();
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
			PrevWeap();
		if (Input.GetButtonDown("Fire2"))
			NextWeap();

	}

	void SetActive(int weapNum)
	{
		foreach (GameObject weap in weapons)
		{
			if (weap == weapons[weapNum])
				weap.SetActive(true);
			else
				weap.SetActive(false);
		}
		activeWeap = weapNum;
	}

	void NextWeap()
	{
		if (activeWeap == weapons.Length - 1)
		{
			SetActive(0);
		}
		else
			SetActive(activeWeap + 1);
	}

	void PrevWeap()
	{
		if (activeWeap == 0)
		{
			SetActive(weapons.Length - 1);
		}
		else
			SetActive(activeWeap - 1);
	}
}
