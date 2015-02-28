using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team : ScriptableObject
{
	public string 		name;
	public Color 		teamColor;
	public bool 		friendlyFire;
	public Planet 		SpawnPoint;
	public List<Player>	players;
	
	public void RemovePlayer(Player p)
	{
		players.Remove (p);
	}
	
	public void AddPlayer(Player p)
	{
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
		return(string.Format("({0},{1},{2},{3})", name, teamColor, friendlyFire, players.ToArray()));
	}
	
}