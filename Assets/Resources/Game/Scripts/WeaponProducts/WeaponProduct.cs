using UnityEngine;
using System.Collections;

public abstract class WeaponProduct : GameComponent {
	public Player ShotBy { get; set; }

	public void Start() {
		OwnedBy = Team.GetTeam(ShotBy);
		Spawned();
	}

	public abstract void Spawned();
	// maybe an onhit function that's called when the projectile hits a player of the opposing team?
	//public abstract void OnHit();
}
