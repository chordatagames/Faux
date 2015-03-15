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
			weaponObject = value;
			Weapon = value.GetComponent<Weapon>();
			Weapon.PickedUpBy = gameObject.GetComponent<Player>();
			GameObject obj = (GameObject) Instantiate(weaponObject,transform.position,Quaternion.identity);
			obj.transform.position = transform.position;
			obj.transform.parent = transform;
		} 
	}

	public Weapon Weapon { get; set; }
}