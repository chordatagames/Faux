using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
//	public AnimationControl animCtrl;

	public float maxSpeed, jumpForce, acc;

	void Start () 
	{
		
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			transform.Rotate(new Vector3( 0, 180));
		}
		if(Input.GetKeyDown(KeyCode.D))
		{
			rigidbody2D.AddForce(new Vector2(0,jumpForce));
		}
	}

	void FixedUpdate () 
	{
		if(Input.GetKey(KeyCode.W))//TODO Check if grounded!
		{
			if(transform.InverseTransformVector(rigidbody2D.velocity).x < maxSpeed)
			{
				rigidbody2D.AddForce(transform.right*acc);
			}
		}
	}
}
