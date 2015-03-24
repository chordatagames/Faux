using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(PlayerController), typeof(WeaponHolder))] //PlayerAnimator is in child
public class Player : Living
{
	public PlayerData playerData;

	public float acceleration;
	public float maxSpeed_TODO; // TODO
	public float jumpForce;
	public string startingWeapon; // TODO - access The WeaponDB and make dropdownmenu for selecting an item from the DB

	public Camera trackCam { get; set; }
	// throwMomentum per weapon makes more sense
	
	public PlayerController pc { get; set; }
	public PlayerAnimator 	pa { get; set; }
	public WeaponHolder		pw { get; set; }

	void Awake()
	{
		OwnedBy = playerData.playerTeam;

		pc = GetComponent<PlayerController>();
		pc.player = this;
		pa = transform.FindChild("Sprite").GetComponent<PlayerAnimator>();
		pa.player = this;
		pw = GetComponent<WeaponHolder>();
	}

	void Start()
	{
		base.sprite = transform.FindChild("Sprite").GetComponent<SpriteRenderer>();
		if (startingWeapon == "NoWeapon")
			print("Starting weapon is NoWeapon"); //there's not a story here, promise
		PickUpWeapon(GameController.WeaponDictionary.Get(startingWeapon));
	}

	public void PickUpWeapon(GameObject weapon)
	{
		pw.WeaponObject = weapon;
		pw.WeaponObject.transform.localScale = Vector3.Scale(pw.WeaponObject.transform.localScale,new Vector3(-1,1,1));
	}
}