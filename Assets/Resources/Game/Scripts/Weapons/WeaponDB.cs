using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDB : ScriptableObject 
{
	public static WeaponDB instance;

	[System.Serializable]
	public class WeaponEntry 
	{
		public string name;
		[Header("GameObject MUST hold Weapon script.")]
		public GameObject weapon;
	}
	
	public static Dictionary<string,GameObject> WeaponDictionary;

	[Header("Always populate dictionary after modifying.")]
	[SerializeField] // Unity says you'll almost never need this. I just did motherfucker.
	private WeaponEntry[] weaponEntries;
	
	public void PopulateDictionary() 
	{
		WeaponDictionary = new Dictionary<string,GameObject>();
		foreach (WeaponEntry we in weaponEntries) 
		{
			WeaponDictionary.Add(we.name,we.weapon);
		}
	}

	void OnEnable() 
	{
		instance = this;
	}
}