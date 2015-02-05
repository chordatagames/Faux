using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour //Weapon handler for players - may ned "genericification".
{
	public Player player { get; set; }

	public Weapons curWpn;
	Weapon usedWeapon;

	delegate void FireActions();
	
	public void FireWeapon()//TODO Move somewhere else
	{
		Transform projectiles = GameObject.Find("Projectiles").transform;
		
		
		GameObject fired = null;
		//USE DELGATES
		switch ( curWpn ) //http://unity3d.com/learn/tutorials/modules/intermediate/scripting/coding-practices
		{
		case Weapons.GRENADE_LAUNCHER:
			fired = (GameObject)Instantiate(Resources.Load<GameObject> ("Game/Prefabs/Grenade") );
			goto default;
		case Weapons.RPG:
			fired = (GameObject)Instantiate(Resources.Load<GameObject> ("Game/Prefabs/Bazooka") );
			goto default;
		case Weapons.MASS_CHANGER:
			fired = (GameObject)Instantiate(Resources.Load<GameObject> ("Game/Prefabs/MassChanger") );
			goto default;
		default:
			break;
		}
		if (fired != null)
		{
			fired.transform.parent = projectiles;
			usedWeapon = fired.GetComponent<Weapon>();// GENERIC! The "Grenade" component is inherited from Weapon.
			usedWeapon.usedBy = gameObject;
			usedWeapon.transform.position = transform.position + (player.facingRight ? transform.right : -transform.right);
		}
	}
}