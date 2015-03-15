using UnityEngine;
using System.Collections;

public class Flag : GameComponent, IPickupComponent
{
	protected bool	_canPickUp 	= false;

	public bool CanPickUp 	{ get { return _canPickUp; } set { _canPickUp = value; } }
	public bool PickedUp 	{ get { return HeldBy != null; } }
	public Player HeldBy 	{ get; set; } 
	public Team HeldByTeam { get { return Team.GetTeam ( HeldBy ); } }

	public void PickUp( Player player )
	{
		//TODO - Pickups?
		HeldBy = player;
	}

	public static Flag SpawnFlag( Planet spawnBase )
	{
		GameObject flagObject = (GameObject)Instantiate(Resources.Load<GameObject>("Game/Prefabs/Flag"));

		return new Flag ();
	}
}
