using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//FIXME -- Many Settings are invalid
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
	public int timer		= 0;
	public int asteriods	= 0;
	//=======GAMEOPTIONS========
	
	bool started = false;

	List<Team> teams = new List<Team>();
	Team[] Teams {get { return teams.ToArray(); } }

	List<Player> players = new List<Player>();
	Player[] Players {get { return players.ToArray(); } }

	int playersInTeams = 0;
	
	void OnGUI()
	{
		if (!started)
		{
	//PLAYER UTILITIES
			if (GUI.Button( new Rect (25, 25, 100, 25), "Add Player") )
			{
				players.Add(Instantiate( Resources.Load<GameObject> ("Game/Prefabs/Player")).GetComponent<Player>());
			}
			if (GUI.Button( new Rect (125, 25, 100, 25), "Remove Player") )
			{
				if (Players.Length > 2)
				{
					Destroy(Players[Players.Length-1].gameObject);
					players.RemoveAt(Players.Length-1);
				}
			}
			GUI.TextField ( new Rect (25, 50, 200, 25), "Players: " + Players.Length.ToString() );


	//TEAM UTILITIES
			if (GUI.Button( new Rect (225, 25, 100, 25), "Add Team") )
			{
				if(Teams.Length < Players.Length)
				{
					teams.Add(ScriptableObject.CreateInstance<Team>());
				}
			}
			if (GUI.Button( new Rect (325, 25, 100, 25), "Remove Team") )
			{
				if (Teams.Length > 2)
				{
					teams.RemoveAt(Teams.Length-1);
				}
			}
			GUI.TextField ( new Rect (225, 50, 200, 25), "Teams: " + Teams.Length.ToString() );

	//REMAINING PLAYERS TO PLACE IN TEAMS
			GUI.TextField ( new Rect (225, 75, 200, 25), "Unteamed players: " + (Players.Length-playersInTeams) );

	// TEAM BOXES
			for (int i = 0; i < Teams.Length; i++)
			{
				Teams[i].name = GUI.TextField ( new Rect (425+i*100, 0, 100, 25), Teams[i].name );
				GUI.Box(new Rect(425 + i*100,25,100,100), Teams[i].name);
				
				Teams[i].teamColor.r = GUI.HorizontalSlider(new Rect(425 + i*100,25,100,25),Teams[i].teamColor.r,0.0f,1.0f);
				Teams[i].teamColor.g = GUI.HorizontalSlider(new Rect(425 + i*100,50,100,25),Teams[i].teamColor.g,0.0f,1.0f);
				Teams[i].teamColor.b = GUI.HorizontalSlider(new Rect(425 + i*100,75,100,25),Teams[i].teamColor.b,0.0f,1.0f);
				
				if(GUI.Button( new Rect (425+i*100, 100, 40, 25), "+"))
				{
					if(playersInTeams < Players.Length)
					{
						playersInTeams++;
						Teams[i].AddPlayer(Players[Players.Length-playersInTeams]);
					}
				}
				if(GUI.Button( new Rect (465+i*100, 100, 40, 25), "-"))
				{
					if (playersInTeams > 0)
					{
						playersInTeams--;
						Teams[i].RemovePlayer( Teams[i].Players[Teams[i].Players.Length-1] );
					}
				}
				GUI.TextField ( new Rect(505+i*100, 100, 20, 25), Teams[i].Players.Length.ToString() );
				
			}
			
			if (GUI.Button (new Rect (25, 75, 200, 25), "Start") )
			{
				if(playersInTeams < Players.Length)
				{
					if (Teams.Length == 0)
					{
						foreach (Player p in Players)
						{
							Team t = ScriptableObject.CreateInstance<Team>();
							t.name = p.name;
							t.AddPlayer(p);
							teams.Add(t);
						}
					}
					for (int i = 0; i < Players.Length-playersInTeams; i++)
					{
						Players[i].OwnedBy = Teams[i%Teams.Length]; //Place reminding players in teams
					}
				}

				started = true;
				//SET PLAYERS TO TEAMS AND UPDATE PLAYERS AND TEAMS TO CURRENT
				GameOptions.Teams = Teams.Length;
				GameOptions.Players = Players.Length;
				SetupPlayers();
			}
		}
		
	}

	void Start ()
	{
		//A MULTIPLAYER GAME NEEDS AT LEAST TWO PLAYERS AND TWO TEAMS.
		players.Add(Instantiate( Resources.Load<GameObject> ("Game/Prefabs/Player")).GetComponent<Player>());
		players.Add(Instantiate( Resources.Load<GameObject> ("Game/Prefabs/Player")).GetComponent<Player>());
		teams.Add(ScriptableObject.CreateInstance<Team>());
		teams.Add(ScriptableObject.CreateInstance<Team>());

		GameObject projectilesParent = new GameObject();
		projectilesParent.name = "Projectiles";
		
		WorldOptions.GravityScale       = gravityScale;
		WorldOptions.Planets            = planets;
		WorldOptions.WorldSize          = worldSize;
		
		if (generateGame)
		{
			SpawnPlanets ();
			SetupSpawnPoints ();
			SetupFlags ();
		}
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
//		foreach ( Team t in World.Teams ) //One flagspawn per team, in their bases/spawnpoints
//		{
//			SpawnFlag( t.SpawnPoint ); //Function in Flag-class?
//		}
	}

	void SetupCapturePoints()
	{
		for (int i = 0; i < Mathf.Min(GameOptions.CapturePoints, World.Planets.Length); i++)
		{
//			World.Planets[i].
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
			player.pa.UpdateColors();
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