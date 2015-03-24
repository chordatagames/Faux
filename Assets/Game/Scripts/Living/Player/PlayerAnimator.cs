using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour 
{
	public Player player { get; set; }

	Animator anim;
	Transform parent;
	
	public float maxIdleTime = 40.0f, idleTime;

	void Start () 
	{
		anim = GetComponent<Animator>();
		parent = transform.parent;
		player = parent.GetComponent<Player>();
	}

	public void UpdateColors ()
	{
		GetComponent<SpriteRenderer>().material.SetColor("_Color", player.OwnedBy.teamColor);
	}

	void Update () 
	{
		transform.localRotation = Quaternion.Euler( new Vector3(0,(player.facingRight ? 0 : 180), 0));

		anim.SetFloat("horizontalVelocity", transform.InverseTransformDirection(parent.GetComponent<Rigidbody2D>().velocity).x);
		anim.SetFloat("verticalVelocity", transform.InverseTransformDirection(parent.GetComponent<Rigidbody2D>().velocity).y);
		anim.SetBool("grounded", player.grounded);

		if(idleTime < 0)
		{
			anim.SetInteger("idleActivity", Random.Range(1,3));
			idleTime = Random.Range(0.0f, maxIdleTime);
		}
		else
		{
			anim.SetInteger("idleActivity", 0);
			idleTime -= Time.deltaTime;
		}
	}
}
