using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Recording : MonoBehaviour
{
	public static Recording gameRecording;

	MatchRecord matchRecord = new MatchRecord();

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (Input.GetKey(KeyCode.RightShift))
			{
				ClearRecording();
			}
			else
			{
				if (IsInvoking("CreateFrame"))
				{
				    PauseRecording();
				}
				else
				{
					StartRecording ();
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.Backspace) && (!Input.GetKey(KeyCode.RightShift)))
		{
			SaveRecording();
		}
	}
	
	void CreateFrame ()
	{
		int i = 0;
		foreach (GameObject playerObject in World.Players)
		{
			Transform player = playerObject.transform;
			PlayerSnapshot temp = new PlayerSnapshot(player.position.x, player.position.y, player.position.z, player.rotation.x, player.rotation.y, player.rotation.z, player.rotation.w);

			if (matchRecord.playerRecords.Count <= i)
				matchRecord.playerRecords.Add(new PlayerRecord());
			matchRecord.playerRecords[i].snapshots.Add (temp);
			i++;
		}
	}

	void StartRecording(string recName = "NewRecording")
	{
		Debug.Log ("Started recording");
		InvokeRepeating ("CreateFrame", 0.0f, 0.012f);
	}

	void PauseRecording()
	{
		Debug.Log ("Stopped recording");
		CancelInvoke();
	}

	void ClearRecording()
	{
		Debug.Log ("Cleared recording");
		matchRecord = new MatchRecord();
	}

	void SaveRecording()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.dataPath + "/Recordings/LastRecording.dat");
		Debug.Log ("Saved recording to " + Application.dataPath + "/Recordings/LastRecording.dat");

		bf.Serialize (file, matchRecord);
		file.Close ();
	}
};

[Serializable]
public class MatchRecord
{
	public MatchRecord () {playerRecords = new List<PlayerRecord>();}
	public List<PlayerRecord> playerRecords;
};

[Serializable]
public class PlayerRecord
{
	public PlayerRecord() {snapshots = new List<PlayerSnapshot>();}
	public List<PlayerSnapshot> snapshots;
};

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
};
















