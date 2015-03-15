using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDB : ScriptableObject 
{
	[System.Serializable]
	public class WeaponEntry 
	{
		public string name;
		[Header("Must have script that inherits Weapon.")]
		public GameObject weapon;
	}

	public static Dictionary<string,GameObject> WeaponDictionary;

	[SerializeField] // Unity says you'll almost never need this. I just did motherfucker.
	private WeaponEntry[] weaponEntries;

	public void PopulateDictionary() {
		WeaponDictionary = new Dictionary<string,GameObject>(); //*toilet flushing sound*
		foreach (WeaponEntry we in weaponEntries) 
		{
			WeaponDictionary.Add(we.name,we.weapon);
		}
	}
}