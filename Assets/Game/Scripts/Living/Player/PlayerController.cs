using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	private string[] Axes = {"Flip", "Jump", "Fire", "Acc", "AccAxis"};
	private Rigidbody2D rb;
	private Player player;

	void Start()
	{
		player = GetComponent<Player>();
		for (int i=0; i<Axes.Length; i++)
		{
			Axes[i] += player.playerData.playerID;
		}

		rb = GetComponent<Rigidbody2D>();
	}

	void Update() 
	{
		if(Input.GetButtonDown(Axes[0]))
		{
			player.facingRight = !player.facingRight;
			player.pw.WeaponObject.transform.localScale = Vector3.Scale(player.pw.WeaponObject.transform.localScale,new Vector3(-1,1,1));
		}
		if(Input.GetButtonDown(Axes[1]) && player.grounded)
		{
			rb.AddForce(transform.TransformPoint( new Vector2(0,player.jumpForce*GetComponent<Rigidbody2D>().mass)));
		}
		if (Input.GetButtonDown(Axes[2]))
		{
			player.pw.Weapon.FireWeapon();
		}
	}

	void FixedUpdate() 
	{
		if (player.grounded)
		{
			if (Input.GetButton(Axes[3]) || Input.GetAxisRaw(Axes[4]) != 0)
			{
//				if(transform.InverseTransformVector(rigidbody2D.velocity).x < maxSpeed)
//				{
				Debug.DrawRay(transform.position, (player.facingRight ? transform.right : -transform.right),Color.magenta);
				rb.AddForce((player.facingRight ? transform.right : -transform.right) * player.acceleration);
//				}
			}
			GetComponent<Rigidbody2D>().drag = 0.075f;
		}
		else
		{
			GetComponent<Rigidbody2D>().drag = 0.01f;
		}
	}
}
