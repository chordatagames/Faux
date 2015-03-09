using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class Recording : MonoBehaviour
{
	public static Recording gameRecording;

	PlayerRecord playerRecord;

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Return) && !IsInvoking("CreateFrame"))
		{
			CreateRecording ();
		}
		if (Input.GetKeyDown (KeyCode.Backspace))
		{
			SaveRecording();
		}
	}
	
	void CreateFrame ()
	{
		Transform temp = World.Players[0].transform;
		playerRecord.transforms.Add (temp);
	}

	void CreateRecording(string recName = "NewRecording")
	{
		Debug.Log ("Started Recording");
		InvokeRepeating ("CreateFrame", 0.0f, 0.2f);
	}

	void SaveRecording()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.dataPath + "/Recordings/LastRecording");
		Debug.Log ("Saved recording to " + Application.dataPath + "/Recordings/LastRecording");
	}
}

[Serializable]
class PlayerRecord
{
	public List<Transform> transforms;
}








