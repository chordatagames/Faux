using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

	public float maxSpeed, jumpForce, speed;
//	[HideInInspector()]
	public bool grounded = false, facingRight = true;
	public LayerMask groundMask;
	public Transform groundChild;

	public enum Weapon
	{
		GRENADE_LAUNCHER,
		TIME_CONTROLLER
	}
	public Weapon curWpn;
	Throwable usedThrowable;

	void Update () 
	{
		if(Input.GetButtonDown("Flip"))
		{
			facingRight = !facingRight;
		}
		if(Input.GetButtonDown("Jump") && grounded)
		{
			rigidbody2D.AddForce( transform.TransformVector( new Vector2(0,jumpForce) ) );
		}
		if ( Input.GetButtonDown("Fire") )
		{
			FireWeapon(curWpn);
		}
	}

	void FixedUpdate () 
	{
		if (grounded)
		{
			if(Input.GetButton("Accelerate") || Input.GetAxisRaw("Accelerate") !=0)
			{
				//if(transform.InverseTransformVector(rigidbody2D.velocity).x < maxSpeed)
				//{
					Debug.DrawRay(transform.position, (facingRight ? transform.right : -transform.right),Color.magenta);
					rigidbody2D.AddForce( (facingRight ? transform.right : -transform.right) * speed );
				//}
			}
			rigidbody2D.drag = 0.075f;
		}
		else
		{
			rigidbody2D.drag = 0.0025f;
		}
		grounded = Physics2D.Raycast(groundChild.position, -transform.up, 0.1f, groundMask);
	}

	void FireWeapon(Weapon wpnUsed)
	{
		GameObject fired;
		
		if (wpnUsed == Weapon.GRENADE_LAUNCHER)
		{
			fired = (GameObject)Instantiate(Resources.Load<GameObject> ("Game/Prefabs/Grenade") );
			usedThrowable = fired.GetComponent<Grenade>();
			usedThrowable.transform.position = transform.position + (facingRight ? transform.right : -transform.right) + transform.up;
			usedThrowable.spawnTransform = transform;
		}
	}
}
