using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public abstract class WeaponProduct : GameComponent {
	public Player ShotBy { get; set; }
	public float damage;

	public void Start() 
	{
		OwnedBy = Team.GetTeam(ShotBy);
		Spawned();
	}

	/// <summary>
	/// Raises the trigger enter2 d event. (THANKS MONODEVELOP)
	/// A lot of this might not apply to certain weapon products, so...
	/// just modify it if that happens, I guess?
	/// </summary>
	void OnTriggerEnter2D(Collider2D other) 
	{
		Living living;
		if ((living = other.gameObject.GetComponent<Living>()) == null) 
		{
			print("Non-living object "+other.name+" hit by "+name);
			OnHit(other.gameObject);
		}
		// check if player hit is relevant
		if ((ShotBy.OwnedBy == living.OwnedBy && living.OwnedBy.friendlyFire) || living.OwnedBy != ShotBy.OwnedBy) 
		{
			living.health -= damage;
			OnHit(living);
		}
	}

	protected abstract void Spawned();
	// v these line up SO NICE
	protected abstract void OnHit(Living living); // called when player is hit
	protected abstract void OnHit(GameObject gc); // called when anything else is hit
}
