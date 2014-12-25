using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour 
{

	Animator anim;
	PlayerController PC;
	Transform parent;
	
	public float maxIdleTime = 40.0f, idleTime;

	void Awake () 
	{
		anim = GetComponent<Animator>();

		parent = transform.parent;
		PC = parent.GetComponent<PlayerController>();
	}

	void Update () 
	{
		anim.SetFloat("horizontalVelocity", transform.InverseTransformDirection(parent.rigidbody2D.velocity).x);
		anim.SetFloat("verticalVelocity", transform.InverseTransformDirection(parent.rigidbody2D.velocity).y);
		anim.SetBool("grounded", PC.grounded);

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
