using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class World
{
	//THESE LISTS WILL BE FILLED DURING GAME SETUP TODO
	public static List<Team> 				listTeams 		= new List<Team> ();
	public static List<Player>				listPlayers 	= new List<Player> ();//filled while in 'lobby'
	public static List<Weapon>				listWeapon 		= new List<Weapon> ();
	public static List<GravityAttractor>	listAttractors 	= new List<GravityAttractor> ();
	public static List<Planet>				listPlanets 	= new List<Planet> ();
	
	public static GameObject[] Players			{ get{ return GameObject.FindGameObjectsWithTag("Player"); } }
	public static GameObject[] Projectiles		{ get{ return GameObject.FindGameObjectsWithTag("Projectile"); } }
	public static GameObject[] GravityAttractors{ get{ return GameObject.FindGameObjectsWithTag("GravityAttractor"); } }

	public static Planet[] Planets	{ get { return listPlanets.ToArray (); } }

	public static Team[] Teams		{ get { return listTeams.ToArray (); } }

	public static GameObject[] OtherPlayers		( params GameObject[] playerArgs ) 
	{
		List<GameObject> others = new List<GameObject> ();
		if (playerArgs.Length == 1)
		{
			foreach (GameObject p in World.Players)
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
	public static float 	GravityScale	{ get; set; }
	public static Vector2 	WorldSize		{ get; set; }
	public static int 		Planets			{ get; set; }
}