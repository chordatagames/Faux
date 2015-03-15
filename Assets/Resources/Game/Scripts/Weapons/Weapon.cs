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

	private float cooldownTime = 0; 

	public void Start() 
	{
		//OwnedBy = Team.GetTeam(PickedUpBy);
		Spawned();
	}

	public void Update()
	{
		if (cooldownTime > 0) cooldownTime -= Time.deltaTime;
	}

	public void FireWeapon() 
	{
		if (cooldownTime <= 0) 
		{
			GameObject _product = (GameObject)Instantiate(product, transform.position, Quaternion.identity);
			_product.GetComponent<WeaponProduct>().ShotBy = PickedUpBy;
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