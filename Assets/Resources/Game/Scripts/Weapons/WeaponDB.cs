using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDB : ScriptableObject 
{
	[System.Serializable]
	public class WeaponEntry
	{
		public string name; // I got a null reference exception on this, so...
		[Header("GameObject MUST hold Weapon script.")]
		public GameObject weapon;
	}

	[SerializeField]
	private List<WeaponEntry> weaponEntries = new List<WeaponEntry>(); 
	private WeaponEntry[] WeaponEntries { get { return weaponEntries.ToArray(); } }

	// This is just a lot fucking simpler ya feel me brostoevsky?
	public GameObject Get(string name) 
	{
		foreach (WeaponEntry e in WeaponEntries) 
		{
			if (e.name == name) return e.weapon;
		} 
		return null;
	}
}