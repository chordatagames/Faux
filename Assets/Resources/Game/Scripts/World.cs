using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class World
{
	public static GameObject[] players{ get{ return  GameObject.FindGameObjectsWithTag("Player"); } }
	public static GameObject[] GravityAttractors{ get{ return GameObject.FindGameObjectsWithTag("GravityAttractor"); } }

	public static GameObject[] OtherPlayers( params GameObject[] playerArgs ) 
	{
		List<GameObject> others = new List<GameObject> ();
		if (playerArgs.Length == 1)
		{
			foreach (GameObject p in players)
			{
				if (p != playerArgs[0])
				{
					others.Add(p);
				}
			}
		}
		else
		{
			for (int i=0; i < playerArgs.Length; i++)
			{
				foreach (GameObject p in OtherPlayers(playerArgs[i]))
				{
					if (p != playerArgs[i])
					{
						others.Add(p);
					}
				}
			}
		}
		return others.ToArray ();
	}
}
