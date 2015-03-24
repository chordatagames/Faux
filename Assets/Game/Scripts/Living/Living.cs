using UnityEngine;
using System.Collections;

public abstract class Living : GameComponent 
{
	public float health;
	public static float DAMAGE_COLOUR_TIME = 0.3f; // manually setting what should be consistent was annoying

	public bool dead 		{ get; set; }
	public bool grounded 	{ get; set; }
	public bool facingRight { get; set; }

	protected bool invulnerable;
	protected SpriteRenderer sprite;

	protected virtual void Awake()
	{
		sprite = GetComponent<SpriteRenderer>();
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
		dead = true; // what's the point of setting a var when it will be removed and inaccessible just after...
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

	IEnumerator DamageColour() {
		invulnerable = true;
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		sprite.color = Color.red;
		yield return new WaitForSeconds(DAMAGE_COLOUR_TIME);
		sprite.color = Color.clear;
		invulnerable = false;
	}
}
