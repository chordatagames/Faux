using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class World
{
	public static List<Team> 				teams 			= new List<Team> ();
	public static List<Player>				listPlayers 	= new List<Player> ();
	public static List<Weapon>				listWeapon 		= new List<Weapon> ();
	public static List<GravityAttractor>	listAttractors 	= new List<GravityAttractor> ();


	public static GameObject[] players{ get{ return  GameObject.FindGameObjectsWithTag("Player"); } }
	public static GameObject[] projectiles{ get{ return  GameObject.FindGameObjectsWithTag("Projectile"); } }
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

public static class WorldOptions
{
	public static float 	GravityScale;
	public static Vector2 	WorldSize;
	public static int 		Planets;
}