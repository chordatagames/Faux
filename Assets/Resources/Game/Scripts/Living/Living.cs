using UnityEngine;
using System.Collections;

public abstract class Living : GameComponent 
{
	public float health = 100;

	public bool dead 		{ get; set; }
	public bool grounded 	{ get; set; }
	public bool facingRight { get; set; }

	protected virtual void Update ()
	{
		if (health <= 0)
		{
			Kill ();
		}
	}

	public virtual void Kill () 
	{
		dead = true;
		Destroy (gameObject);
	}

	public virtual void Damage (float dmgTaken)
	{
		health -= dmgTaken;
	}
}
