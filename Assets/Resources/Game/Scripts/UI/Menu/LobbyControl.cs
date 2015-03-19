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
	List<PlayerData> playerDatas = new List<PlayerData>();
	public PlayerData[] PlayerDatas {get { return playerDatas.ToArray(); } }

	List<TeamUIControl> teamUIs = new List<TeamUIControl>();
	TeamUIControl[] TeamUIs {get { return teamUIs.ToArray(); } }

	public InputField playerCount;
	public InputField notInTeams;
	public Transform teamBox;

	public GameObject teamUIPrefab;
	public PlayerData defaultPlayerData;

	
	/// <summary>
	/// Start this instance.
	/// Set up a basic game with 2 playerDatas and 2 teams.
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
		foreach(PlayerData pd in PlayerDatas)
		{
			World.listPlayers.Add( pd.InstantiatePlayer().GetComponent<Player>() );
		}
	}


	public void AddPlayer()
	{
		//TODO - Set player name based on input-controller/client (set up in their playerTab?)
		//TODO - Set player ID

		PlayerData pd = ScriptableObject.CreateInstance<PlayerData>();
		pd.Init( defaultPlayerData );
		playerDatas.Add(pd);

		UpdatePlayerCount();
	}
	
	public void RemovePlayer()//Only removable if not in team already
	{
		if (PlayerDatas.Length > 2 && (PlayerDatas.Length-GetUnteamed().Length) >= 0 )
		{
			Destroy(PlayerDatas[PlayerDatas.Length-1]);
			playerDatas.RemoveAt( PlayerDatas.Length-1 );
		}
		UpdatePlayerCount();
	}
	
	public void AddTeam()
	{
		if( TeamUIs.Length < PlayerDatas.Length && GetUnteamed().Length > 0 )
		{
			GameObject newTeamUI = (GameObject)Instantiate<GameObject>(teamUIPrefab);
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

	public PlayerData[] GetUnteamed()
	{
		List<PlayerData> unteamed = new List<PlayerData>();
		foreach(PlayerData pd in PlayerDatas)
		{
			if (pd.playerTeam == pd.initTeam) //Is player owned by Default Team - equivalent to 'unteamed'.
			{
				unteamed.Add(pd);
			}
		}
		return unteamed.ToArray();
	}

	//Update UI

	public void UpdatePlayerCount()
	{
		playerCount.text = PlayerDatas.Length.ToString();
		notInTeams.text = GetUnteamed().Length.ToString();
	}
}
