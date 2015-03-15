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
	Player[] Players {get { return players.ToArray(); } }

	List<Team> teams = new List<Team>();
	Team[] Teams {get { return teams.ToArray(); } }

	List<TeamUIControl> teamUIs = new List<TeamUIControl>();
	TeamUIControl[] TeamUIs {get { return teamUIs.ToArray(); } }

	public InputField playerCount;
	public InputField notInTeams;


	/// <summary>
	/// Start this instance.
	/// Set up a basic game with 2 players and 2 teams.
	/// </summary>
	void Start()
	{
		for(int i = 0; i < 2; i++)
		{
			players.Add(Instantiate( Resources.Load<GameObject> ("Game/Prefabs/Player")).GetComponent<Player>());
			teams.Add(ScriptableObject.CreateInstance<Team>());
		}

		UpdatePlayerCount();
		UpdateNotInTeams();
	}

	public void UpdatePlayerCount()
	{
		playerCount.text = Players.Length.ToString();
	}

	public void UpdateNotInTeams()
	{
		int i = 0;
		foreach( Team t in Teams )
		{
			i += t.Players.Length;
		}

		notInTeams.text = ( Players.Length - i ).ToString();
	}



	public void AddPlayer()
	{
		players.Add(Instantiate( Resources.Load<GameObject> ("Game/Prefabs/Player")).GetComponent<Player>());
	}

	public void RemovePlayer()
	{
		if (Players.Length > 2)
		{
			Destroy( Players[ Players.Length-1 ].gameObject );
			players.RemoveAt( Players.Length-1 );
		}
	}

	public void AddTeam()
	{
		if(Teams.Length < Players.Length)
		{
			teams.Add(ScriptableObject.CreateInstance<Team>());
			teamUIs.Add(Instantiate( Resources.Load<GameObject> ("Game/Prefabs/UI/Menu/TeamUI")).GetComponent<TeamUIControl>());
		}
	}

	public void RemoveTeam()
	{
		if (Teams.Length > 2)
		{
			teams.RemoveAt(Teams.Length-1);
		}
	}

}
