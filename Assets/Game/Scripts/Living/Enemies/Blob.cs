using UnityEngine;
using System.Collections;

public class Blob : Living 
{
	public float acceleration;
	public float topSpeed;
	public float damage;

	private Rigidbody2D rigidbody;
	private SpriteRenderer sprite;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		facingRight = Random.Range(0f,1f) > 0.5f;
	}

	void FixedUpdate()
	{
		if (rigidbody.velocity.magnitude < topSpeed) 
		{
			rigidbody.velocity = (facingRight ? transform.right : -transform.right) * acceleration;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Living living;
		if ((living = collision.gameObject.GetComponent<Living>()) != null)
		{
			living.Damage(damage,this);
		}
	}
}