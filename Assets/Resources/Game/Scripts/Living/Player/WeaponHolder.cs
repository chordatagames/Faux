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
			obj.transform.parent = transform;
			obj.transform.position = obj.transform.parent.position;
		} 
	}

	public Weapon Weapon { get; set; }
}