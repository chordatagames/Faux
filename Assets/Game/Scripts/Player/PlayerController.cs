using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

	public float maxSpeed, jumpForce, speed;
	public bool grounded = false;
	public LayerMask groundMask;


	void Start () 
	{
		transform.localPosition += new Vector3(0, 0.5f, 0);
	}

	void Update () 
	{
		if(Input.GetButtonDown("Flip"))
		{
			transform.Rotate(0,180,0);
		}
		if(Input.GetButtonDown("Jump") && grounded)
		{
			rigidbody2D.AddForce( transform.TransformVector( new Vector2(0,jumpForce) ) );
		}
	}

	void FixedUpdate () 
	{
		if (grounded)
		{
			if(Input.GetButton("Accelerate") || Input.GetAxisRaw("Accelerate") !=0)
			{
				if(transform.InverseTransformVector(rigidbody2D.velocity).x < maxSpeed)
				{
					rigidbody2D.AddForce(transform.right * speed);
				}
			}
			rigidbody2D.drag = 0.075f;
		}
		else
		{
			rigidbody2D.drag = 0.0025f;
		}
		grounded = Physics2D.Raycast(transform.position, -transform.up, 0.1f, groundMask);
	}
}
