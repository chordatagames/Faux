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

	protected bool HasShot { get { return (cooldownTime > 0); } }
	protected float cooldownTime;

	void Start() 
	{
		cooldownTime = 0; //CAN SHOOT
		//OwnedBy = Team.GetTeam(PickedUpBy);
		Spawned();
	}

	 void Update()
	{
		if (HasShot) 
		{
			cooldownTime -= Time.deltaTime;
			Debug.Log(cooldown+", "+cooldownTime);
		}
	}

	public void FireWeapon() 
	{
		if (!HasShot)
		{
			GameObject _product = (GameObject) Instantiate(product, transform.position, Quaternion.identity);
			//_product.GetComponent<WeaponProduct>().ShotBy = PickedUpBy;
			if (amountOfUses != -1) amountOfUses--;

			cooldownTime = cooldown;
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