using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(PlayerController), typeof(WeaponHolder))] //PlayerAnimator is in child
public class Player : Living
{
	public Team initTeam; //ONLY USE FOR SETTING THE INITIAL TEAM, Use Owned by otherwise

	public static int playerCount = 0;
	public int playerID { get; set; }
	public float acceleration;
	public float maxSpeed_TODO;//TODO
	public float jumpForce;

	public Camera trackCam { get; set; }
	// throwMomentum per weapon makes more sense
	
	public PlayerController pc { get; set; }
	public PlayerAnimator 	pa { get; set; }
	public WeaponHolder		pw { get; set; }
	
	void Awake ()
	{
		playerID = playerCount;
		playerCount++;

		OwnedBy = initTeam;

		pc = GetComponent<PlayerController>();
		pc.player = this;
		pa = transform.FindChild("Sprite").GetComponent<PlayerAnimator>();
		pa.player = this;
		pw = GetComponent<WeaponHolder>();
		pw.weaponScript.PickedUpBy = this;
	}
}