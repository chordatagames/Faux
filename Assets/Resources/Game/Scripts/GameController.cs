using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour //TODO make static
{
	//public Team[] teams = new Team[GameOptions.Teams];

	public bool generateGame = false;

	//=========WORLDOPTIONS=========
	public float 	gravityScale= 1;
	public int 		planets 	= 4;
	public Vector2	worldSize 	= new Vector2 ( 30, 30 ); //Unit-"boundaries"
	//=========WORLDOPTIONS=========

	//=======GAMEOPTIONS========
	public int flags		= 0;
	public int capturePoints= 0;
	public int teamCount	= 0;
	public int timer		= 0;
	public int asteriods	= 0;
	public int players		= 0;
	//=======GAMEOPTIONS========
	
	void Start ()
	{
		GameObject projectilesParent = new GameObject();
		projectilesParent.name = "Projectiles";

		WorldOptions.GravityScale 	= gravityScale;
		WorldOptions.Planets		= planets;
		WorldOptions.WorldSize		= worldSize;

		GameOptions.Players			= players;
		GameOptions.Asteriods		= asteriods;
		GameOptions.CapturePoints 	= capturePoints;
		GameOptions.Flags			= flags;
		GameOptions.Teams			= teamCount;
		GameOptions.Timer 			= timer;

		if (generateGame) 
		{
			SpawnPlanets ();
			SetupSpawnPoints ();
			SetupFlags ();
			
			SpawnPlayers ();
			

		}
		SetupPlayers ();
	}

	void SpawnPlanets() //Temporary code for planet-generation
	{
		for (int x=0; x*10 < WorldOptions.WorldSize.x; x++)
		{
			for (int y=0; y*10 < WorldOptions.WorldSize.y; y++)
			{
				if((x+1)*(y+1) <= WorldOptions.Planets)
				{
					GameObject planet = (GameObject)Instantiate( Resources.Load<GameObject> ("Game/Prefabs/Planet") );
					planet.transform.localScale = Vector3.one * WorldOptions.WorldSize.x*Random.value/WorldOptions.Planets;
					planet.transform.position = new Vector3 (x*10, y*10);
				}
			}
		}
	}

	/*==================SETUP===================
	 * ONLY TAKE USE OF ALREADY SPAWNED OBJECTS
	 * E.G. ONLY USE THE 'WORLD'-CLASS WITH ITS
	 * PROPERTIES
	 */

	void SetupSpawnPoints()
	{
		for(int i = 0; i < World.Teams.Length; i++)
		{
			World.Teams[i].SpawnPoint = World.Planets[i%World.Planets.Length]; //Sets a spawn for teams, if teams > planets then set spawn for >1 teams to the same.
		}
	}

	void SetupFlags()
	{
		foreach ( Team t in World.Teams ) //One flagspawn per team, in their bases/spawnpoints
		{
			//SpawnFlag( t.SpawnPoint ); //Function in Flag-class?
		}
	}

	void SetupCapturePoints()
	{
		for (int i = 0; i < Mathf.Min(GameOptions.CapturePoints, World.Planets.Length); i++)
		{
//			World.Planets[i].
		}
	}


	void SpawnPlayers()
	{
		for (int p = 0; p < GameOptions.Players; p++)
		{
			//GameObject player = (GameObject)
			Instantiate( Resources.Load<GameObject> ("Game/Prefabs/Player") );
		}
	}

	//-------------------------------------------------------------------
	
	void SetupPlayers()
	{
		Rect[] screens = GetSplitScreens();
		Player player;
		PlayerCamera pCam;
		for (int i=0;i < World.Players.Length; i++)
		{
			GameObject p = World.Players[i];
			player = p.GetComponent<Player>();
			player.trackCam = new GameObject ("Player_" + player.playerID + "_cam", typeof(Camera), typeof(PlayerCamera)).GetComponent<Camera>();
			player.trackCam.rect = screens[i];
			pCam = player.trackCam.GetComponent<PlayerCamera>();
			pCam.tracking = player.gameObject;
		}
	}

	Rect[] GetSplitScreens()
	{
		List<Rect> screens = new List<Rect>();
		float players = World.Players.Length;
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
	public static int Players		{ get; set; }
	public static int Flags			{ get; set; }
	public static int CapturePoints	{ get; set; }
	public static int Teams			{ get; set; } // - if zero, teams = playercount, teamname = playername;
	public static int Timer			{ get; set; } // - timed game, lasting for 'timer' minutes;
	public static int Asteriods		{ get; set; }
}