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
			CreatePlayerGhost();
		}
	}

	PlayerRecord LoadRecording ()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.dataPath + "/Recordings/LastRecording.dat", FileMode.Open);
		Debug.Log ("Loaded recording from " + Application.dataPath + "/Recordings/LastRecording.dat");

		PlayerRecord playerRecord = (PlayerRecord)bf.Deserialize (file);
		file.Close ();

		return playerRecord;
	}

	void CreatePlayerGhost()
	{
		Debug.Log ("Creating player ghost");
		GameObject ghost = Instantiate (Resources.Load<GameObject>("Game/Prefabs/PlayerGhost"));
		ghost.GetComponent<PlayerGhostBehaviour>().playerRecord = LoadRecording();
	}
}
