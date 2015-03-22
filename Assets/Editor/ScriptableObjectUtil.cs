using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableObjectUtil
{
	public static void CreateAsset<T> (string path, string extension) where T : ScriptableObject
	{
		Input.
	}

	/// <summary>
	//	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>
	public static void CreateAsset<T> () where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T> ();
		
		string path = AssetDatabase.GetAssetPath (Selection.activeObject);
		if (path == "") 
		{
			path = "Assets";
		} 
		else if (Path.GetExtension (path) != "") 
		{
			path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
		}
		
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/New " + typeof(T).ToString() + ".asset");
		
		AssetDatabase.CreateAsset (asset, assetPathAndName);

		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}
}

public class TeamAsset
{
	[MenuItem("Assets/Create/Team")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtil.CreateAsset<Team> ();
	}
}

public class InputSystemAsset
{
	[MenuItem("Assets/Create/Input")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtil.CreateAsset<InputSetup> ();
	}
}

public class WeaponDBAsset
{
	[MenuItem("Assets/Create/WeaponDB")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtil.CreateAsset<WeaponDB> ();
	}
}

public class PlayerDataAsset //ONLY USED ONCE REALLY, TO OBTAIN THE "Default PlayerData as an asset"
{
	[MenuItem("Assets/Create/PlayerData")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtil.CreateAsset<PlayerData> ();
	}
}
