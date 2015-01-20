using UnityEngine;
using System.Collections;

public class Living : MonoBehaviour, ILiving 
{
	public float health = 100;
	public bool dead = false;

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
		health += dmgTaken;
	}
}
