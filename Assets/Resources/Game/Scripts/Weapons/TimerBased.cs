using UnityEngine;
using System.Collections;

public class TimerBased : Shooting
{
	public float timer; 

	public override void Wait ()
	{
		if (timer < 6 && ((int)timer) % 2 == 0) 
		{
			GetComponent<Renderer>().material.color = Color.red;
		}
		else
		{
			GetComponent<Renderer>().material.color = Color.yellow;
		}

		timer -= 0.5f;
		if(timer < 0)
		{
			Activate ();
		}
	}

	public override void Activate ()
	{
		base.Activate();
		Destroy (this.gameObject);
	}
}
