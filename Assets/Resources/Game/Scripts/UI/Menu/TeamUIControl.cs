using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TeamUIControl : MonoBehaviour
{
	private Team team;
	public int playersInTeam { get { return team.Players.Length; } }
	public InputField teamName;
	public Slider red, green, blue;
	public InputField playersInTeamField;
	LobbyControl lobby;

	void Awake()
	{
		team = ScriptableObject.CreateInstance<Team>();
	}

	void Start()
	{
		lobby = transform.parent.parent.GetComponent<LobbyControl>();
		UpdatePlayersInTeam();
	}

	public void AddPlayerToTeam() //TODO The input-controller's own player joins this team
	{
		if (lobby.UnteamedPlayers().Length > 0) //Anyone to add
		{
			team.AddPlayer( lobby.UnteamedPlayers()[lobby.UnteamedPlayers().Length-1] );
			UpdatePlayersInTeam();
		}
		Debug.Log("There are currently " + lobby.UnteamedPlayers().Length + " unteamed");
	}

	
	public void RemovePlayerFromTeam() //TODO The input-controller's own player leaves this team
	{
		if (team.Players.Length > 0) //Anyone to remove
		{
			team.RemovePlayer( team.Players[team.Players.Length-1] );
			UpdatePlayersInTeam();
		}
	}
	
	//UPDATE UI
	public void UpdateLobbyPlayerCount()
	{
		lobby.UpdatePlayerCount();
	}

	public void UpdateTeamName()
	{
		team.name = teamName.text;
	}
	public void UpdateTeamColor()
	{
		team.teamColor = new Color(red.value, green.value, blue.value);
	}
	public void UpdatePlayersInTeam()
	{
		playersInTeamField.text = playersInTeam.ToString();
	}
}