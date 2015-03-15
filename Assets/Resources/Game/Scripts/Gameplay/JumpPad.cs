using UnityEngine;
using System.Collections;

public class JumpPad : GameComponent, IAlignable
{
	private float _allignRadius = 5;

	public LayerMask affectingLayers;
	public float pushForce = 1200;
	public float alignRadius { get { return _allignRadius; } set { _allignRadius = value; } }

	void OnTriggerEnter2D(Collider2D col)
	{
		if( !onlyOwnerUse || col.GetComponent<GameComponent>().OwnedBy == OwnedBy) 
		{
			if (((1<<col.gameObject.layer) & affectingLayers) != 0)
			{
				// Will push all objects just as hard regardless of mass
				col.GetComponent<Rigidbody2D>().AddForce(pushForce * col.GetComponent<Rigidbody2D>().mass * transform.up); 
			}
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = FauxEditorGizmoSettings.allignGizmoColor;
		Gizmos.DrawSphere(transform.position, 5);
	}

	public void AlignTo(Collider2D col)
	{
		if (col.GetType() == typeof(CircleCollider2D))
		{
			Vector2 fromCenterDir = (transform.position - col.transform.position).normalized;
			transform.position = (Vector2)col.transform.position + fromCenterDir * col.transform.localScale.x/2;

			float angle = Vector2.Angle(Vector2.right, -fromCenterDir) * Mathf.Deg2Rad*Mathf.Sign(-fromCenterDir.y) + Mathf.PI/2;
			transform.rotation = new Quaternion( 0, 0, 1 * Mathf.Sin ( angle/2 ), Mathf.Cos ( angle/2 ) );
		}
	}

}

public static class FauxEditorGizmoSettings
{
	public static Color allignGizmoColor = new Color (0, 1, 0, 0.3f);
}
