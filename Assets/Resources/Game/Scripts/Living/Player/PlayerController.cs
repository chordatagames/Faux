﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Living
{
	public static int playerCount = 0;

	[HideInInspector()]
	public int playerID = 0;


	public float acceleration;
	public float maxSpeed;
	public float jumpForce;
	public float throwMomentum;  //Rename to throwForce, perhaps?
	private string[] Axes = {"Flip", "Jump", "Fire", "Acc", "AccAxis"};

	public Camera trackCam { get; set; }
	
	public LayerMask groundMask;
	float distToGround;

	public Weapons curWpn;
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
		distToGround = collider2D.bounds.extents.y;
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
		grounded = Physics2D.Raycast(transform.position, -transform.up, distToGround+0.15f, groundMask);
	}

	void FireWeapon(Weapons wpnUsed)//TODO Move somewhere else
	{
		Transform projectiles = GameObject.Find("Projectiles").transform;
		GameObject fired = null;
		//USE DELGATES
		switch ( wpnUsed ) //http://unity3d.com/learn/tutorials/modules/intermediate/scripting/coding-practices
		{
		case Weapons.GRENADE_LAUNCHER:
			fired = (GameObject)Instantiate(Resources.Load<GameObject> ("Game/Prefabs/Grenade") );
			usedThrowable = fired.GetComponent<Grenade>();
			usedThrowable.throwDir = (facingRight ? transform.right : -transform.right)+transform.up/2;
			goto default;
		case Weapons.RPG:
			fired = (GameObject)Instantiate(Resources.Load<GameObject> ("Game/Prefabs/Bazooka") );
			usedThrowable = fired.GetComponent<Bazooka>();
			Debug.Log(usedThrowable);
			usedThrowable.throwDir = (facingRight ? transform.right : -transform.right);
			goto default;
		case Weapons.MASS_CHANGER:
			fired = (GameObject)Instantiate(Resources.Load<GameObject> ("Game/Prefabs/MassChanger") );
			usedThrowable = fired.GetComponent<MassChanger>();
			usedThrowable.throwDir = (facingRight ? transform.right : -transform.right);
			goto default;
		default:
			if (fired != null)
				fired.transform.parent = projectiles;
			usedThrowable.rigidbody2D.velocity = rigidbody2D.velocity + throwMomentum * usedThrowable.throwDir / usedThrowable.rigidbody2D.mass;
			rigidbody2D.velocity -= throwMomentum * usedThrowable.throwDir / rigidbody2D.mass;
			usedThrowable.transform.position = transform.position + (facingRight ? transform.right : -transform.right);
			break;
		}
	}
}
