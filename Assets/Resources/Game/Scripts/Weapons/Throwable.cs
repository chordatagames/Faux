using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Throwable : MonoBehaviour , IWeaponThrowable
{
	[HideInInspector]
	public float bonusThrowMomentum;
	public Vector2 throwDir;
	public bool activated = false;
	
	public virtual void Start () 
	{
		rigidbody2D.velocity += ( throwDir * bonusThrowMomentum ) / rigidbody2D.mass;
		Spawned ();
	}
	
	public virtual void Spawned ()
	{
		StartCoroutine ( WaitingRoutine( 0.5f ) );
	}

	protected virtual IEnumerator WaitingRoutine(float interval)
	{
		while (!activated)
		{
			Wait(); //Functionality
			yield return new WaitForSeconds(interval);
		}
	}

	public virtual void Wait ()
	{
	}
	public virtual void Activate ()
	{
		activated = true;
		StartCoroutine (ActiveRoutine (0.5f));
		//Destroy (this.gameObject);
	}

	protected virtual IEnumerator ActiveRoutine(float interval)
	{
		while (activated)
		{
			Active(); //Functionality
			yield return new WaitForSeconds(interval);
		}
	}
	
	public virtual void Active ()
	{
	}
}