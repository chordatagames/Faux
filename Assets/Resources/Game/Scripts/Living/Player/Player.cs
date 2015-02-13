using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
	public string 		name;
	public Color 		teamColor;
	public bool 		friendlyFire;
	public Player[] 	players;

	public Team(string name, Color teamCol) : this(name, teamCol, false)
	{
	}
	public Team(string name, Color teamCol, bool ff) : this()
	{
		this.name = name;
		this.teamColor = teamCol;
		this.friendlyFire = ff;
		this.players = new Player[0];
	}

	public void RemovePlayer(Player p)
	{
		List<Player> pList = new List<Player> ();
		foreach ( Player player in players )
		{
			pList.Add(player);
		}
		pList.Remove (p);
		players = pList.ToArray ();
	}

	public void AddPlayer(Player p)
	{
		List<Player> pList = new List<Player> ();
		foreach ( Player player in players )
		{
			pList.Add(player);
		}
		pList.Add(p);
		players = pList.ToArray ();
	}

	public static Team GetTeam (string name)
	{
		foreach(Team t in World.teams.ToArray())
		{
			if (t.name == name)
			{ return t; }
		}
		return new Team();
	}
	public static Team GetTeam (Player player)
	{
		foreach(Team t in World.teams.ToArray())
		{
			foreach(Player p in t.players)
			{
				if(p == player)
				{return t;}
			}
		}
		return new Team();
	}

	public override string ToString()
	{
		return(string.Format("({0},{1},{2},{3})", name, teamColor, friendlyFire, players));
	}
	
}