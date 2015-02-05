using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour 
{
	public Player player { get; set; }

	Animator anim;
	Transform parent;
	
	public float maxIdleTime = 40.0f, idleTime;

	void Awake () 
	{
		anim = GetComponent<Animator>();
		parent = transform.parent;
	
		//Temporary placement of code, TODO when starting game, players may choose color.
		GetComponent<SpriteRenderer>().material.SetColor("_Color", new Color(Random.value, Random.value, Random.value));
	}

	void Update () 
	{

		transform.localRotation = Quaternion.Euler( new Vector3(0,(player.facingRight ? 0 : 180), 0));

		anim.SetFloat("horizontalVelocity", transform.InverseTransformDirection(parent.rigidbody2D.velocity).x);
		anim.SetFloat("verticalVelocity", transform.InverseTransformDirection(parent.rigidbody2D.velocity).y);
		anim.SetBool("grounded", player.grounded);

		if(idleTime < 0)
		{
			anim.SetInteger("idleActivity", Mathf.RoundToInt(Random.Range(1,3)));
			idleTime = Random.Range(0.0f, maxIdleTime);
		}
		else
		{
			anim.SetInteger("idleActivity", 0);
			idleTime -= Time.deltaTime;
		}
	}
}
