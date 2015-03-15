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
	public Player PickedUpBy { get; set; } //Can possibly be changed to type of 'Player'
	/// <summary>
	/// The amount of times a weapon can be used before it is EXTERMINATED.
	/// </summary>
	public int amountOfUses;
	public float cooldown;

	public WeaponProduct product; 

	public void Start() 
	{
		OwnedBy = Team.GetTeam(PickedUpBy);
		Spawned();
	}

	/// <summary>
	/// Called on spawn of weapon. Amount of uses and cooldown must be specified.
	/// </summary>
	public abstract void Spawned();
	public virtual void FireWeapon() {
		WeaponProduct wp = (WeaponProduct) Instantiate(product, transform.position, Quaternion.identity);
		wp.ShotBy = PickedUpBy; // does this make a lick of sense i am so tired
	}
}