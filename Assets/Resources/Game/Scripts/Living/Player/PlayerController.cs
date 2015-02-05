using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public Player player { get; set; }

	private string[] Axes = {"Flip", "Jump", "Fire", "Acc", "AccAxis"};
	public LayerMask groundMask;
	float distToGround;
	
	void Awake ()
	{
		for (int i=0; i<Axes.Length; i++)
		{
			Axes[i] += player.playerID;
		}
		name += player.playerID;
		distToGround = collider2D.bounds.extents.y;
	}
	void Update () 
	{
		if(Input.GetButtonDown(Axes[0]))
		{
			player.facingRight = !player.facingRight;
		}
		if(Input.GetButtonDown(Axes[1]) && player.grounded)
		{
			rigidbody2D.AddForce( transform.TransformPoint( new Vector2(0,player.jumpForce*rigidbody2D.mass) ) );
		}
		if ( Input.GetButtonDown(Axes[2]) )
		{
			player.pw.FireWeapon();
		}
	}

	void FixedUpdate () 
	{
		if (player.grounded)
		{
			if(Input.GetButton(Axes[3]) || Input.GetAxisRaw(Axes[4]) !=0)
			{
//				if(transform.InverseTransformVector(rigidbody2D.velocity).x < maxSpeed)
//				{
				Debug.DrawRay(transform.position, (player.facingRight ? transform.right : -transform.right),Color.magenta);
				rigidbody2D.AddForce( (player.facingRight ? transform.right : -transform.right) * player.acceleration );
//				}
			}
			rigidbody2D.drag = 0.075f;
		}
		else
		{
			rigidbody2D.drag = 0.01f;
		}
		player.grounded = Physics2D.Raycast(transform.position, -transform.up, distToGround+0.15f, groundMask);
	}
}
