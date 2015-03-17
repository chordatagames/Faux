using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// peh peh someone else try
/// </summary>
//[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
/*	int choiceIndex = 0;
	public override void OnInspectorGUI()
	{
		// Draw the default inspector
		DrawDefaultInspector();
		var player = (Player) target;
		WeaponDB wdb = (WeaponDB) AssetDatabase.LoadAssetAtPath("Assets/Resources/Game/Scripts/Weapons",typeof(WeaponDB));
		string[] choices = new string[wdb.WeaponEntries.Length]; 
		for (int i = 0; i < choices.Length; i++) choices[i] = wdb.WeaponEntries[i].name;
		choiceIndex = EditorGUILayout.Popup(choiceIndex, choices);

		// Update the selected choice in the underlying object
		player.startingWeapon = choices[choiceIndex];
		// Save the changes back to the object
		EditorUtility.SetDirty(target);
	}*/
}
