using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team : ScriptableObject
{
	public Color            teamColor = Color.white;
	public bool             friendlyFire = false;
	public Planet           SpawnPoint = null;
	List<Player>            players = new List<Player>();
	public Player[]         Players{ get{ return players.ToArray(); } }
	
	public void RemovePlayer(Player p)
	{
		p.OwnedBy = p.initTeam;
		players.Remove (p);
	}

	public void SpawnPlayer() //OBSOLETE
	{
		GameObject player = (GameObject)Instantiate( Resources.Load<GameObject> ("Game/Prefabs/Player"));
		Player p = player.GetComponent<Player> ();
		p.OwnedBy = this;
		p.initTeam = this;
		AddPlayer (p);
		
	}

	public void AddPlayer(Player p)
	{
		p.OwnedBy = this;
		p.pa.UpdateColors();
		Debug.Log ("Added Player: " + p);
		players.Add(p);
	}
	
	public static Team GetTeam (string name) //Unreliable in some situations?
	{
		foreach(Team t in World.Teams)
		{
			if (t.name == name)
			{ return t; }
		}
		return null;
	}
	public static Team GetTeam (Player player)
	{
		return player.OwnedBy;
	}
	
	public override string ToString()
	{
		return(string.Format("({0},{1},{2},{3})", name, teamColor, friendlyFire, players.ToArray().ToString()));
	}
}