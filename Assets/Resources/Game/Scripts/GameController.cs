using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour 
{
	public float gravityScale = 1;

	void Start ()
	{
		GameObject projectilesParent = new GameObject();
		projectilesParent.name = "Projectiles";

		Player player;
		PlayerCamera pCam;
		Rect[] screens = GetSplitScreens();
		//splitScreen ();

		for (int i=0;i < World.players.Length; i++)
		{
			GameObject p = World.players[i];
			player = p.GetComponent<Player>();
			player.trackCam = new GameObject ("Player_" + player.playerID + "_cam", typeof(Camera), typeof(PlayerCamera)).camera;
			player.trackCam.rect = screens[i];
			pCam = player.trackCam.GetComponent<PlayerCamera>();
			pCam.tracking = player.gameObject;
		}
	}

	Rect[] GetSplitScreens()
	{
		List<Rect> screens = new List<Rect>();
		float players = World.players.Length;
		int rows = Mathf.RoundToInt (Mathf.Sqrt (players));
		int colsInRow;

		for (int y=0; y < rows; y++)
		{
			colsInRow = Mathf.CeilToInt(players/(rows-y));
			for (int x=0; x < colsInRow; x++)
			{
				screens.Add(GetScreenRect(x, y, colsInRow, rows));
			}
			players -= colsInRow;
		}
		return screens.ToArray();
	}

	Rect GetScreenRect(float x, float y, int colsInRow, int rows)
	{	//Because the camera viewport is in normalized values, I don't need to use Screen.<width/height>
		return new Rect( x/colsInRow, y/rows, 1f/colsInRow, 1f/rows);
	}
}


