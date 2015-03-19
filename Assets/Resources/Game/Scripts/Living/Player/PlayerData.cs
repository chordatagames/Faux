using UnityEngine;
using System.Collections;

public sealed class PlayerData : ScriptableObject
{
	public static int playerCount;
	public Team initTeam;

	[HideInInspector]
	public Team 	playerTeam;
	
	public string 	playerName 	= "new Player";	//Set in the inspector
	public int		playerID 	= 0;
	
	public GameObject playerPrefab;

	public void Init( PlayerData defaultPlayerData )
	{
		initTeam 		= defaultPlayerData.initTeam;
		playerTeam		= initTeam;
		playerName 		= defaultPlayerData.playerName;
		playerID		= defaultPlayerData.playerID;
		playerPrefab	= defaultPlayerData.playerPrefab;
	}

	public GameObject InstantiatePlayer()
	{
		Debug.Log("Uh");
		Player player = GameObject.Instantiate<GameObject>(playerPrefab).GetComponent<Player>();
		player.name = playerName;
		player.playerData = this;
		return player.gameObject;
	}
}