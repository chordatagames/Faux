using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(WeaponDB))]
public class WeaponDBEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		WeaponDB wdb = (WeaponDB)target;
		if(GUILayout.Button("Populate Dictionary"))
		{
			wdb.PopulateDictionary();
		}
	}
}