using UnityEngine;
using System.Collections;

public class JumpPad : GameComponent, IAlignable
{
	private float _alignRadius = 5;

	public LayerMask affectingLayers;
	public float pushForce = 1200;
	public float alignRadius { get { return _alignRadius; } set { _alignRadius = value; } }

	void OnTriggerEnter2D(Collider2D col)
	{
		if(!onlyOwnerUse || col.GetComponent<GameComponent>().OwnedBy == OwnedBy) 
		{
			if (((1<<col.gameObject.layer) & affectingLayers) != 0)
			{
				// Will push all objects just as hard regardless of mass
				if (col.GetComponent<Rigidbody2D>() != null)
					col.GetComponent<Rigidbody2D>().AddForce(pushForce * col.GetComponent<Rigidbody2D>().mass * transform.up); 
			}
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = FauxEditorGizmoSettings.alignGizmoColor;
		Gizmos.DrawSphere(transform.position, 5);
	}

	public void AlignTo(Collider2D col)
	{
		if (col.GetType() == typeof(CircleCollider2D) || col.GetType() == typeof(BoxCollider2D))
		{
			Vector2 fromCenterDir = ((Vector2)(transform.position - col.transform.position)).normalized;
			RaycastHit2D hit = Physics2D.Raycast (transform.position, -fromCenterDir, alignRadius, 1 << 9 | 1 << 10);
			transform.position = hit.point;
			Vector2 dir = -hit.normal;

			float angle = Vector2.Angle(Vector2.right, dir) * Mathf.Deg2Rad * Mathf.Sign(dir.y) + Mathf.PI/2;
			transform.rotation = new Quaternion( 0, 0, Mathf.Sin ( angle/2 ), Mathf.Cos ( angle/2 ) );
		}
	}

}

public static class FauxEditorGizmoSettings
{
	public static Color alignGizmoColor = new Color (0, 1, 0, 0.3f);
}
