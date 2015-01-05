using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Throwable : MonoBehaviour 
{
	public float timer, throwForce;

	[HideInInspector]
	public Transform spawnTransform;
	
	void Start () 
	{
		rigidbody2D.AddForce ( (spawnTransform.right+spawnTransform.up/2).normalized * throwForce );
		Spawned ();
	}
	
	void Update () 
	{
		timer-=Time.deltaTime;
		if(timer < 0)
		{
			Activate ();
		}
	}
	
	public virtual void Spawned ()
	{
		StartCoroutine ("Living", 2);
	}
	public virtual void Living ()
	{
	}
	public virtual void Activate ()
	{
		Destroy (this.gameObject);
		StopCoroutine ("Living");
	}
}