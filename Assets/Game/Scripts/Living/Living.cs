using UnityEngine;
using System.Collections;

public abstract class Living : GameComponent 
{
	public const float DAMAGE_COLOUR_TIME = 0.5f; // manually setting what should be consistent for every prefab was annoying

	public float health;
	public LayerMask groundMask;

	public bool dead 		{ get; set; }
	public bool grounded 	{ get; set; }
	public bool facingRight { get; set; }

	protected bool invulnerable;
	protected SpriteRenderer sprite;

	private float distToGround;

	protected virtual void Awake()
	{
		sprite = GetComponent<SpriteRenderer>();
	}

	protected virtual void Start() 
	{
		distToGround = GetComponent<Collider2D>().bounds.extents.y;
	}

	protected virtual void Update()
	{
		if (health <= 0)
		{
			Kill();
		}
	}

	public virtual void Kill()
	{
		dead = true; // what's the point of setting a var when it will be removed & inaccessible just after...
		Destroy(gameObject); 
	}

	public virtual void Damage(float dmgTaken, GameComponent gc)
	{
		if (!invulnerable && OwnedBy != gc.OwnedBy) 
		{
			health -= dmgTaken;
			StartCoroutine("DamageColour");
		}
	}

	void FixedUpdate() 
	{
		grounded = Physics2D.Raycast(transform.position, -transform.up, distToGround+0.15f, groundMask);
	}

	IEnumerator DamageColour() 
	{
		invulnerable = true;
		Color hold = sprite.material.GetColor("_Color");
		sprite.material.SetColor("_Color",Color.red);
		yield return new WaitForSeconds(DAMAGE_COLOUR_TIME);
		sprite.material.SetColor("_Color",hold);
		invulnerable = false;
	}
}
