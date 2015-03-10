using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Recording : MonoBehaviour
{
	public static Recording gameRecording;

	PlayerRecord playerRecord = new PlayerRecord();

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Return) && !IsInvoking("CreateFrame"))
		{
			CreateRecording ();
		}
		if (Input.GetKeyDown (KeyCode.Backspace) && (!Input.GetKey(KeyCode.RightShift)))
		{
			SaveRecording();
		}
	}
	
	void CreateFrame ()
	{
		Transform t = World.Players[0].transform;
		PlayerSnapshot temp = new PlayerSnapshot(t.position.x, t.position.y, t.position.z, t.rotation.x, t.rotation.y, t.rotation.z, t.rotation.w);
		playerRecord.snapshots.Add (temp);
	}

	void CreateRecording(string recName = "NewRecording")
	{
		Debug.Log ("Started Recording");
		InvokeRepeating ("CreateFrame", 0.0f, 0.012f);
	}

	void SaveRecording()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.dataPath + "/Recordings/LastRecording.dat");
		Debug.Log ("Saved recording to " + Application.dataPath + "/Recordings/LastRecording.dat");

		bf.Serialize (file, playerRecord);
		file.Close ();
	}
}

[Serializable]
public class PlayerRecord
{
	public PlayerRecord() {snapshots = new List<PlayerSnapshot>();}
	public List<PlayerSnapshot> snapshots;
}

[Serializable]
public class PlayerSnapshot //TODO: Find better solution to Vector3 and Quaternions being non-serializable
{
	public PlayerSnapshot(float x_, float y_, float z_, float i_, float j_, float k_, float w_) {x = x_; y = y_; z = z_; i = i_; j = j_; k = k_; w = w_;}

	public float x;
	public float y;
	public float z;

	public float i;
	public float j;
	public float k;
	public float w;
}
















