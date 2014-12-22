using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

	public float maxSpeed, jumpForce, speed;
	public bool grounded = false;
	public LayerMask groundMask;
	float distToGround;


	void Start () 
	{
		distToGround = collider2D.bounds.extents.y + 0.1f;
		
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			transform.Rotate(new Vector3( 0, 180));
		}
		if(Input.GetKeyDown(KeyCode.D) && grounded)
		{
			rigidbody2D.AddForce( transform.TransformVector( new Vector2(0,jumpForce) ) );
		}
	}

	void FixedUpdate () 
	{
		if(Input.GetKey(KeyCode.W) && grounded)
		{
			if(transform.InverseTransformVector(rigidbody2D.velocity).x < maxSpeed)
			{
				rigidbody2D.AddForce(transform.right * speed * Time.deltaTime);
			}
		}
		grounded = Physics2D.Raycast(transform.position, -transform.up, distToGround, groundMask);
	}
}
