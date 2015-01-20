using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour 
{


	void Start ()
	{
		PlayerController pc;
		PlayerCamera pCam;

		//splitScreen ();

		for (int i=0;i < World.players.Length; i++)
		{
			GameObject player = World.players[i];
			pc = player.GetComponent<PlayerController>();
			pc.trackCam = new GameObject ("Player_" + pc.playerID + "_cam", typeof(Camera), typeof(PlayerCamera)).camera;

			pCam = pc.trackCam.GetComponent<PlayerCamera>();
			pCam.tracking = pc.gameObject;
		}
	}
	//TODO fix all
	Vector2[][] splitScreen()
	{
		List<int> SHET = new List<int>();
		int rows = Mathf.RoundToInt (Mathf.Sqrt (World.players.Length)); //ROWS
		for (int i=0; i < rows; i++)
		{
		//	for (int j=0; j < (World.players.Length % rows) ? World.players.Length / rows : (int)(World.players.Length / rows)+1; j++)
		//	{
				//GET VECTOR
		//	}
		}
		int collumns = World.players.Length / rows;//COLLUMNS
		return null;
	}

}
