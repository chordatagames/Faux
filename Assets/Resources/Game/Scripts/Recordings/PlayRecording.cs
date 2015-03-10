using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayRecording : MonoBehaviour
{

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Backspace) && Input.GetKey (KeyCode.RightShift))
		{
			CreatePlayerGhosts();
		}
	}

	MatchRecord LoadRecording ()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.dataPath + "/Recordings/LastRecording.dat", FileMode.Open);
		Debug.Log ("Loaded recording from " + Application.dataPath + "/Recordings/LastRecording.dat");

		MatchRecord matchRecord = (MatchRecord)bf.Deserialize (file);
		file.Close ();

		return matchRecord;
	}

	void CreatePlayerGhosts()
	{
		Debug.Log ("Creating player ghosts");
		foreach (PlayerRecord playerRecord in LoadRecording().playerRecords)
		{
			GameObject ghost = Instantiate (Resources.Load<GameObject>("Game/Prefabs/PlayerGhost"));
			ghost.GetComponent<PlayerGhostBehaviour>().playerRecord = playerRecord;
		}
	}
}
