using UnityEngine;
using System.Collections;

/// <summary>
/// F1X TH1S
/// </summary>
public class Blob : Living 
{
	public float acceleration;
	public float topSpeed;
	public float damage;

	[SerializeField]
	private Team team;
	private Rigidbody2D rb;
	
	protected override void Start ()
	{
		base.Start();
		rb = GetComponent<Rigidbody2D>();
		OwnedBy = team;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Living living;
		if ((living = collision.gameObject.GetComponent<Living>()) != null)
		{
			living.Damage(damage,this);
		}
	}
}