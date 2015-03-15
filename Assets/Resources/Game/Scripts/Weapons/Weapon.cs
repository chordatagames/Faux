using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * TODO Insert generic weapon functionalities and properties.
 * All weapons will derive from this class.
 * A set of types of weapons can be specified with interfaces, 
 * making it possible to implement multiple weapon functionalities.
 */
[RequireComponent(typeof(GravityPulled))]
public class Weapon : ScriptableObject
{
	public Player PickedUpBy { get; set; } //Can possibly be changed to type of 'Player'
	/// <summary>
	/// The amount of times a weapon can be used before it is EXTERMINATED.
	/// </summary>
	public int amountOfUses;
	public float cooldown;
	public Sprite sprite;
	public Weapon script;
	public GameObject product; 

	public void Start() 
	{
		Spawned();
	}

	public virtual void Spawned()
	{
		ScriptableObject.CreateInstance<Weapon>();
	}
	public virtual void FireWeapon() 
	{
		WeaponProduct wp = (WeaponProduct) Instantiate(product, PickedUpBy.transform.position, Quaternion.identity);
		wp.ShotBy = PickedUpBy; // does this make a lick of sense i am so tired
	}
}