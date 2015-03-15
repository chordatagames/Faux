using UnityEngine;
using System.Collections;

/*
 * TODO Insert generic weapon functionalities and properties.
 * All weapons will derive from this class.
 * A set of types of weapons can be specified with interfaces, 
 * making it possible to implement multiple weapon functionalities.
 */
[RequireComponent(typeof(GravityPulled))]
public abstract class Weapon : GameComponent
{
	public Player usedBy { get; set; }

//	public virtual delegate void Action;
	public virtual void Start() 
	{
		OwnedBy = Team.GetTeam (usedBy);
		GetComponent<Rigidbody2D>().velocity = usedBy.GetComponent<Rigidbody2D>().velocity;
		Spawned ();
	}
	
	public virtual void Spawned ()
	{
	}
}

public enum Weapons
{
	GRENADE_LAUNCHER,
	MASS_CHANGER,
	RPG
}

