using UnityEngine;
using System.Collections;

public abstract class Living : MonoBehaviour, ILiving 
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

	// Use this for initialization
	public virtual void Kill () 
	{
		dead = true;
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	public virtual void Damage (float dmgTaken)
	{
		health -= dmgTaken;
	}
}
