using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TeamControl : MonoBehaviour
{
	private Team team;
	private int playersInTeam = 0;

	private LobbyControl lobby;

	public InputField teamName;
	public Slider red, green, blue;
	public InputField playersInTeamField;

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
		if (lobby.GetUnteamed().Length > 0) //Anyone to add
		{
			lobby.GetUnteamed()[lobby.GetUnteamed().Length-1].playerTeam = team;
			playersInTeam++;
//			team.AddPlayer( lobby.GetUnteamed()[lobby.GetUnteamed().Length-1] );

			UpdatePlayersInTeam();
		}
		Debug.Log("There are currently " + lobby.GetUnteamed().Length + " unteamed");
	}

	
	public void RemovePlayerFromTeam() //TODO The input-controller's own player leaves this team
	{
		if ( lobby.PlayerDatas.Length > lobby.GetUnteamed().Length) //Anyone to remove
		{
			foreach (PlayerData pd in lobby.PlayerDatas)
			{
				if (pd.playerTeam == team) //Is player owned by the team removing players
				{
					pd.playerTeam = pd.initTeam;
					playersInTeam--;
					break;
				}
			}
			UpdatePlayersInTeam();
		}
	}

	public void RemoveTeam()
	{
		if(lobby.GetUnteamed().Length > 0 && lobby.TeamUIs.Length > 2 )
		{
			for(int i = 0; i <= playersInTeam; i++)
			{
				RemovePlayerFromTeam();
			}
			ScriptableObject.Destroy(team);
			team = null;
			lobby.teamUIs.RemoveAt(lobby.teamUIs.IndexOf(this));
			Destroy(gameObject);

			UpdateLobbyPlayerCount();
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