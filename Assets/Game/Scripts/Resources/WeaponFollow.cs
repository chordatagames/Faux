using UnityEngine;
using System.Collections;

/// <summary>
/// This script will attach a transform (assumed to be a weapon)
/// to a living entity. It supports offset and bobbing.
/// It's currently missing the ability to invert the sprite on x-axis
/// when living entity is facing left.
/// I don't know how you want to go about that so I left that out.
/// </summary>
public class WeaponFollow : MonoBehaviour
{
	private const float BOB_INSENSITIVITY = 10f;
	private const float BOB_SPEED = .3f;
	private const float BOB_AMOUNT = 0f;

	private Living target = null;
	private Rigidbody2D targetRigid = null;
	private Vector2 offset = Vector2.zero;

	private float bobTime = 0;

	void LateUpdate()
	{
		// If no target, abort and remove component.
		if (target == null)
		{
			Debug.LogWarning("WeaponFollow script had no target. Script should be created using WeaponFollow.Attach()");
			Destroy(this); // Destroy this component
			return;
		}

		// Calcualte the offset and bobbing
		Vector2 usableOffset = new Vector2(
			target.facingRight ? offset.x : -offset.x,
			offset.y + Mathf.Sin(bobTime * BOB_SPEED) * BOB_AMOUNT
		);

		// The bob time
		bobTime += Mathf.Min(Mathf.Abs(targetRigid.GetRelativePointVelocity(Vector2.right).x), BOB_INSENSITIVITY) / BOB_INSENSITIVITY;

		// Apply position and rotation
		transform.position = target.transform.position + target.transform.TransformVector(usableOffset);
		transform.rotation = target.transform.rotation;
	}

	/// <summary>
	/// Attaches a transform (assumed to be a weapon) to a living target.
	/// It will follow the target around and bob when it moves on the x-axis.
	/// </summary>
	/// <param name="weapon">What to attach</param>
	/// <param name="target">The living entity to attach to</param>
	/// <param name="offset">The offset from center. Will be inverted on x if living faces left.</param>
	/// <returns></returns>
	public static WeaponFollow Attach(Transform weapon, Living target, Vector2 offset)
	{
		// Add component to the weapon
		var follow = weapon.gameObject.AddComponent<WeaponFollow>();

		// Assign variables
		follow.target = target;
		follow.targetRigid = target.GetComponent<Rigidbody2D>();
		follow.offset = offset;

		// Return component
		return follow;
	}
}