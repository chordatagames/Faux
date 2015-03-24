using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team : ScriptableObject
{
	public Color            teamColor = Color.white;
	public bool             friendlyFire = false;
	public Planet           SpawnPoint = null;
	private List<Living>     members = new List<Living>();
	public Living[]         Members { get { return members.ToArray(); } }

	public void RemoveMember(Living living) {
		members.Remove(living);
		living.OwnedBy = null;
	}

	public void RemoveMember(Player living)
	{
		living.OwnedBy = living.playerData.initTeam;
		members.Remove(living);
	}

	public void AddMember(Player p)
	{
		p.OwnedBy = this;
		p.pa.UpdateColors();
		members.Add(p);
	}
	
	public static Team GetTeam (string name) // Unreliable in some situations?
	{
		foreach(Team team in World.Teams)
		{
			if (team.name == name) return team;
		}
		return null;
	}

	public static Team GetTeam (Living living)
	{
		return living.OwnedBy;
	}
	
	public override string ToString()
	{
		return(string.Format("({0},{1},{2},{3})", name, teamColor, friendlyFire, members.ToArray().ToString()));
	}
}