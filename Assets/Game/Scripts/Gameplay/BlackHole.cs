using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour
{
	public GameObject whiteHole;

	void OnTriggerEnter2D (Collider2D col)
	{
		col.transform.position = whiteHole.transform.position;
		RotateVelocity (col.transform);
	}

	void RotateVelocity (Transform a)
	{
		Vector2 _velocity;

		float deltaAngle = whiteHole.transform.eulerAngles.z - transform.eulerAngles.z;
		Debug.Log ("Angle: " + deltaAngle);

		float _angle = (Mathf.Abs (Vector2.Angle (Vector2.right, a.GetComponent<Rigidbody2D>().velocity)) * Mathf.Sign (a.GetComponent<Rigidbody2D>().velocity.y) + deltaAngle) * Mathf.Deg2Rad;

		_velocity.x = Mathf.Cos (_angle);
		_velocity.y = Mathf.Sin (_angle);
		_velocity *= a.GetComponent<Rigidbody2D>().velocity.magnitude;

		a.GetComponent<Rigidbody2D>().velocity = _velocity;
	}
}
