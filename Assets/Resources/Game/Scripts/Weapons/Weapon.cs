using UnityEngine;
using System.Collections;

/*
 * TODO Insert generic weapon functionalities and properties.
 * All weapons will derive from this class.
 * A set of types of weapons can be specified with interfaces, 
 * making it possible to implement multiple weapon functionalities.
 */
public class Weapon : MonoBehaviour
{
	public virtual void Start () 
	{
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

