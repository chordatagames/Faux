using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(PlayerController), typeof(PlayerWeapon))] //PlayerAnimator is in child
public class Player : Living
{

	public PlayerData playerData;

	public static int playerCount = 0;
	public int playerID { get; set; }
	public float acceleration;
	public float maxSpeed_TODO;//TODO
	public float jumpForce;

	public Camera trackCam { get; set; }
	// throwMomentum per weapon makes more sense
	
	public PlayerController pc { get; set; }
	public PlayerAnimator 	pa { get; set; }
	public PlayerWeapon		pw { get; set; }
	
	void Awake ()
	{
		playerID = playerCount;
		playerCount++;

		OwnedBy = playerData.playerTeam;
		Debug.Log("OwnedBy: " + OwnedBy);

		pc = GetComponent<PlayerController>();
		pc.player = this;
		pa = transform.FindChild("Sprite").GetComponent<PlayerAnimator>();
		pa.player = this;
		pw = GetComponent<PlayerWeapon>();
		pw.player = this;
	}

}