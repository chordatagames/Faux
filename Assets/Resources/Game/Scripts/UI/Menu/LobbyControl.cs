using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Lobby control.
/// Used to configure the game setup before game starts and rules & score applies.
/// </summary>
public class LobbyControl : MonoBehaviour
{
	List<Player> players = new List<Player>();
	public Player[] Players {get { return players.ToArray(); } }

	List<TeamUIControl> teamUIs = new List<TeamUIControl>();
	TeamUIControl[] TeamUIs {get { return teamUIs.ToArray(); } }

	public InputField playerCount;
	public InputField notInTeams;
	public Transform teamBox;
	public Canvas canvas;

	
	/// <summary>
	/// Start this instance.
	/// Set up a basic game with 2 players and 2 teams.
	/// </summary>
	void Start()
	{
		for(int i = 0; i < 2; i++)
		{
			AddPlayer();
			AddTeam();
		}
		teamBox.GetChild(0).SetAsLastSibling();
	}

	
	public void StartGame()
	{

	}


	public void AddPlayer()
	{
		players.Add(new PlayerData);
		UpdatePlayerCount();
	}
	
	public void RemovePlayer()//Only removable if not in team already
	{
		if (Players.Length > 2 && (Players.Length-UnteamedPlayers().Length) >= 0 )
		{
			Destroy(Players[ Players.Length-1 ].gameObject);
			players.RemoveAt( Players.Length-1 );
		}
		UpdatePlayerCount();
	}
	
	public void AddTeam()
	{
		if( TeamUIs.Length < Players.Length )
		{
			GameObject newTeamUI = (GameObject)Instantiate( Resources.Load<GameObject>("Game/Prefabs/UI/Menu/TeamUI") );
			newTeamUI.transform.SetParent(teamBox, false);
			teamUIs.Add( newTeamUI.GetComponent<TeamUIControl>() );
		}
	}
	
	public void RemoveTeam()
	{
		if (TeamUIs.Length > 2)
		{
			Destroy(TeamUIs[TeamUIs.Length-1].gameObject);
			teamUIs.RemoveAt(TeamUIs.Length-1);
		}
	}

	public Player[] UnteamedPlayers()
	{
		List<Player> unteamedPlayers = new List<Player>();
		foreach(Player p in Players)
		{
			if (p.OwnedBy == p.initTeam) //Is player owned by Default Team - equivalent to 'unteamed'.
			{
				unteamedPlayers.Add(p);
			}
		}
		return unteamedPlayers.ToArray();
	}

	//Update UI

	public void UpdatePlayerCount()
	{
		playerCount.text = Players.Length.ToString();
		notInTeams.text = UnteamedPlayers().Length.ToString();
	}
}
