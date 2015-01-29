using UnityEngine;
using System.Collections;

public class TimerBased : Throwable
{
	public float timer; 

	public override void Wait ()
	{
		base.Wait ();
		if (timer < 6 && ((int)timer) % 2 == 0) 
		{
			renderer.material.color = Color.red;
		}
		else
		{
			renderer.material.color = Color.yellow;
		}

		timer -= 0.5f;
		if(timer < 0)
		{
			Activate ();
		}
	}

	public override void Activate ()
	{
		base.Activate ();
		Destroy (this.gameObject);
	}
}
