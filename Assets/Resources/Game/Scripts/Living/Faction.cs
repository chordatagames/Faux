using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Faction : ScriptableObject
{
	public string teamName = "New Team";
	public Faction[] enemies;
	public Faction[] friends;

	public Dictionary<Faction, float> factions = new Dictionary<Faction, float>();
	public float defaultRelation = 0;

//	public float relationWith (Faction other)
//	{
//		if (factions.ContainsKey(other))
//		{ return factions[other]; }
//
//		float relation = defaultRelation;
//		foreach (Faction f in friends)
//		{
//			if ()
//		}
//	}
}
