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

	protected bool shooting = false;

	private float cooldownTime;

	public void Start() 
	{
		Debug.Log (cooldownTime);
		cooldownTime = 0;
		//OwnedBy = Team.GetTeam(PickedUpBy);
		Spawned();
	}

	public virtual void Update()
	{
		if (cooldownTime > 0) 
		{
			Debug.Log ("IM TRY ING M U M "+cooldownTime);
			cooldownTime -= Time.deltaTime;
			shooting = true;
		}
		else 
		{
			shooting = false;
		}
	}

	public void FireWeapon() 
	{
		Debug.Log ("shooting: "+shooting+" cooldown time: "+cooldownTime);
		if (!shooting)
		{
			GameObject _product = (GameObject) Instantiate(product, transform.position, Quaternion.identity);
			//_product.GetComponent<WeaponProduct>().ShotBy = PickedUpBy;
			if (amountOfUses != -1) amountOfUses--;
			Debug.Log(cooldown+","+cooldownTime);
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