using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Living
{
	[HideInInspector()]
	public int playerID = 0;
	static int playerCount = 0;
	
	[HideInInspector()]
	public bool grounded = false, facingRight = true;
	public float acceleration;
	public float maxSpeed;
	public float jumpForce;

	private string[] Axes = {"Flip", "Jump", "Fire", "Acc", "AccAxis"};

	private Camera _camera;
	public Camera trackCam { get{ return _camera; } set{ _camera = value; } }
	
	public LayerMask groundMask;
	public Transform groundChild;

	public enum Weapon
	{
		GRENADE_LAUNCHER,
		MASS_CHANGER,
		RPG
	}
	public Weapon curWpn;
	Throwable usedThrowable;

	void Awake ()
	{
		playerID = playerCount;
		playerCount++;

		for (int i=0; i<Axes.Length; i++)
		{
			Axes[i]+=playerID;
		}
		name += playerID;
	}

	protected override void Update () 
	{
		base.Update ();
		if(Input.GetButtonDown(Axes[0]))
		{
			facingRight = !facingRight;
		}
		if(Input.GetButtonDown(Axes[1]) && grounded)
		{
			rigidbody2D.AddForce( transform.TransformPoint( new Vector2(0,jumpForce*rigidbody2D.mass) ) );
		}
		if ( Input.GetButtonDown(Axes[2]) )
		{
			FireWeapon(curWpn);
		}
	}

	void FixedUpdate () 
	{
		if (grounded)
		{
			if(Input.GetButton(Axes[3]) || Input.GetAxisRaw(Axes[4]) !=0)
			{
//				if(transform.InverseTransformVector(rigidbody2D.velocity).x < maxSpeed)
//				{
					Debug.DrawRay(transform.position, (facingRight ? transform.right : -transform.right),Color.magenta);
					rigidbody2D.AddForce( (facingRight ? transform.right : -transform.right) * acceleration );
//				}
			}
			rigidbody2D.drag = 0.075f;
		}
		else
		{
			rigidbody2D.drag = 0.01f;
		}
		grounded = Physics2D.Raycast(groundChild.position, -transform.up, 0.15f, groundMask);
	}

	void FireWeapon(Weapon wpnUsed)
	{
		GameObject fired;

		switch ( wpnUsed )
		{
		case Weapon.GRENADE_LAUNCHER:
			fired = (GameObject)Instantiate(Resources.Load<GameObject> ("Game/Prefabs/Grenade") );
			usedThrowable = fired.GetComponent<Grenade>();
			usedThrowable.throwDir = (facingRight ? transform.right : -transform.right)+transform.up/2;
			goto default;
		case Weapon.RPG:
			fired = (GameObject)Instantiate(Resources.Load<GameObject> ("Game/Prefabs/Bazooka") );
			usedThrowable = fired.GetComponent<Bazooka>();
			Debug.Log(usedThrowable);
			usedThrowable.throwDir = (facingRight ? transform.right : -transform.right);
			goto default;
		case Weapon.MASS_CHANGER:
			fired = (GameObject)Instantiate(Resources.Load<GameObject> ("Game/Prefabs/MassChanger") );
			usedThrowable = fired.GetComponent<MassChanger>();
			usedThrowable.throwDir = (facingRight ? transform.right : -transform.right);
			goto default;
		default:
			usedThrowable.rigidbody2D.velocity = rigidbody2D.velocity;
			usedThrowable.transform.position = transform.position + (facingRight ? transform.right : -transform.right);
			break;
		}
	}
}
