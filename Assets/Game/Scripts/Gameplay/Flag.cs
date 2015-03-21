using UnityEngine;
using System.Collections;

public class Flag : GameComponent, IPickupComponent
{
	protected bool	_canPickUp 	= false;

	public bool CanPickUp 	{ get { return _canPickUp; } set { _canPickUp = value; } }
	public bool PickedUp 	{ get { return HeldBy != null; } }
	public Player HeldBy 	{ get; set; } 
	public Team HeldByTeam { get { return Team.GetTeam ( HeldBy ); } }

	public GameObject flagPrefab;

	public void PickUp( Player player )
	{
		//TODO - Pickups?
		HeldBy = player;
	}
}
