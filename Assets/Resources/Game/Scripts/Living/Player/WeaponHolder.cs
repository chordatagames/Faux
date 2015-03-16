using UnityEngine;
using System.Collections;

public class WeaponHolder : MonoBehaviour {
	private GameObject weaponObject;
	public GameObject WeaponObject { 
		get 
		{
			return weaponObject;
		} 
		set 
		{ 
			GameObject obj = (GameObject) Instantiate(value,transform.position,Quaternion.identity);
			obj.transform.position = transform.position;
			obj.transform.parent = transform;

			weaponObject = obj;
			Weapon = weaponObject.GetComponent<Weapon>();
			Weapon.PickedUpBy = gameObject.GetComponent<Player>();

		} 
	}

	public Weapon Weapon { get; set; }
}