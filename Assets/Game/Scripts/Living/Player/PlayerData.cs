using UnityEngine;
using System.Collections;

public sealed class PlayerData : ScriptableObject
{
	public static int playerCount;//FIXME THIS IS NEVER REDUCED :0
	public Team initTeam;

//	[HideInInspector]
	public Team 	playerTeam;
	
	public string 	playerName;	//Set in the inspector
	public int		playerID;
	
	public GameObject playerPrefab;

	public void Init( PlayerData defaultPlayerData )
	{
		initTeam 		= defaultPlayerData.initTeam;
		playerTeam		= initTeam;
		playerName 		= defaultPlayerData.playerName;
		playerID		= playerCount++;
		playerPrefab	= defaultPlayerData.playerPrefab;
	}

	public GameObject InstantiatePlayer()
	{
		Player player = GameObject.Instantiate<GameObject>(playerPrefab).GetComponent<Player>();
		playerID = playerCount++;
		player.name = playerName;
		player.playerData = this;
		playerTeam.AddPlayer(player);
		return player.gameObject;
	}
}