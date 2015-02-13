using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour //TODO make static
{
	public float gravityScale = 1;
	public Team[] teams = new Team[GameOptions.Teams];

	//===TEMPVALUES=============
	public int flags		= 0;
	public int capturePoints= 0;
	public int teamCount	= 0;
	public int timer		= 0;
	public int asteriods	= 0;
	//===TEMPVALUES=============
	
	void Start ()
	{
		GameObject projectilesParent = new GameObject();
		projectilesParent.name = "Projectiles";

		SetupPlayers();
		WorldOptions.GravityScale = gravityScale;

		GameOptions.Asteriods 		= asteriods;
		GameOptions.CapturePoints 	= capturePoints;
		GameOptions.Flags			= flags;
		GameOptions.Teams			= teamCount;
		GameOptions.Timer 			= timer;

	}

	//-------------------------------------------------------------------
	
	void SetupPlayers()
	{
		Rect[] screens = GetSplitScreens();
		Player player;
		PlayerCamera pCam;
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

public static class GameOptions
{
	public static int Flags			{ get; set; }
	public static int CapturePoints	{ get; set; }
	public static int Teams			{ get; set; } // - if zero, teams = playercount, teamname = playername;
	public static int Timer			{ get; set; } // - timed game, lasting for 'timer' minutes;
	public static int Asteriods		{ get; set; }
}