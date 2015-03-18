using UnityEngine;
using System.Collections;

public abstract class Weapon : GameComponent
{
	public Player PickedUpBy { get; set; }
	/// <summary>
	/// The amount of times a weapon can be used before it is EXTERMINATED.
	/// </summary>
	public int amountOfUses;
	public float cooldown;
	public GameObject product;
	public Vector2 offset = Vector2.zero;

	protected bool CanShoot
	{
		get { return (cooldownTime <= 0); }
	}
	protected float cooldownTime;

	void Start()
	{
		cooldownTime = 0; // Cooldown time set to zero, can shoot
		//OwnedBy = Team.GetTeam(PickedUpBy);
		Spawned();
	}

	void Update()
	{
		cooldownTime = Mathf.Max(cooldownTime - Time.deltaTime, 0);
	}

	public void FireWeapon()
	{
		if (CanShoot)
		{
			GameObject _product = (GameObject)Instantiate(product, (Vector3) transform.position + (Vector3) offset, transform.rotation);
			_product.GetComponent<WeaponProduct>().ShotBy = PickedUpBy;
			amountOfUses = Mathf.Max(amountOfUses - 1, -1); // TODO: Why is this being clamped to -1 and not 0?

			cooldownTime = cooldown; // Cooldown
			WeaponFireBehaviour(_product);
		}
	}

	/// <summary>
	/// Called on spawn of weapon. Amount of uses and cooldown must be specified. 
	/// Custom behaviour for spawning.
	/// </summary>
	protected abstract void Spawned();
	/// <summary>
	/// Code ran after method FireWeapon is run. Custom behaviour for weapon.
	/// </summary>
	protected abstract void WeaponFireBehaviour(GameObject product);
}