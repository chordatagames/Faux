using UnityEngine;
using System.Collections;

public class WeaponHolder : MonoBehaviour {
	private ScriptableObject weaponObject;
	public ScriptableObject WeaponObject { 
		get 
		{
			return weaponObject;
		} 
		set 
		{ 
			weaponObject = value;
			weaponScript = value.<Weapon>(); // I don't know C# let's be honest here
		} 
	}

	[HideInInspector] //...
	public Weapon weaponScript;

	// this is a lot uglier than i wanted it to be but whatever. at least it's easy to change.
	public void Start() {
		weaponObject = Weapon.weapons[startingWeapon];
		weaponScript = weaponObject.GetComponent<Weapon>();
	}
}
