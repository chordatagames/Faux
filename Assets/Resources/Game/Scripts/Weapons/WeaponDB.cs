using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDB : ScriptableObject 
{
	[System.Serializable]
	public class WeaponEntry
	{
		public string name { get { return weapon.name; } }
		[Header("GameObject MUST hold Weapon script.")]
		public GameObject weapon;
	}

	[SerializeField]
	private List<WeaponEntry> weaponEntries = new List<WeaponEntry>(); 
	private WeaponEntry[] WeaponEntries { get { return weaponEntries.ToArray(); } }

	public Dictionary<string, GameObject> WeaponDictionary = new Dictionary<string,GameObject>();
	public void PopulateDictionary() 
	{
		WeaponDictionary.Clear();
		foreach (WeaponEntry we in WeaponEntries) 
		{
			WeaponDictionary.Add(we.name,we.weapon);
		}
	}

}