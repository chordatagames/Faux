using UnityEngine;
using System.Collections;
[RequireComponent(typeof(PlayerController), typeof(PlayerWeapon))]
public class Player : Living
{
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

		pc = GetComponent<PlayerController>();
		pc.player = this;
		pa = transform.FindChild("Sprite").GetComponent<PlayerAnimator>();
		pa.player = this;
		pw = GetComponent<PlayerWeapon>();
		pw.player = this;
	}
}

public struct Team
{
	string 		name;
	Color 		teamColor;
	bool 		friendlyFire;
	Player[] 	players;

	public Team(string name, Color teamCol)
	{
		Team(name, teamCol, false);
	}
	public Team(string name, Color teamCol, bool ff)
	{
		this.name = name;
		teamColor = teamCol;
		friendlyFire = ff;
	}


}