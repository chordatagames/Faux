using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableObjectUtil
{
	/// <summary>
	//	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>
	public static void CreateAsset<T>() where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T> ();
		
		string path = AssetDatabase.GetAssetPath (Selection.activeObject);
		if (path == "") 
		{
			path = "Assets";
		} 
		else if (Path.GetExtension (path) != "") 
		{
			path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
		}
		
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/New " + typeof(T).ToString() + ".asset");
		
		AssetDatabase.CreateAsset(asset, assetPathAndName);
		
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}
}

public class TeamAsset
{
	[MenuItem("Assets/Create/Team")]
	public static void CreateAsset()
	{
		ScriptableObjectUtil.CreateAsset<Team>();
	}
}

public class WeaponDBAsset
{
	[MenuItem("Assets/Create/WeaponDB")]
	public static void CreateAsset()
	{
		ScriptableObjectUtil.CreateAsset<WeaponDB> ();
	}
}