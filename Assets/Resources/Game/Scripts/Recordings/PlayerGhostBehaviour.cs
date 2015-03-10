using UnityEngine;
using System.Collections;

public class PlayerGhostBehaviour : MonoBehaviour
{
	public PlayerRecord playerRecord;
	int frame = 0;

	void Start()
	{
		InvokeRepeating ("NextFrame", 0, 0.012f);
	}

	void NextFrame()
	{
		if (frame >= playerRecord.snapshots.Count)
		{
			Destroy (gameObject);
		}
		else
		{
			PlayerSnapshot snap = playerRecord.snapshots [frame];
			transform.position = new Vector3 (snap.x, snap.y, snap.z);
			transform.rotation = new Quaternion (snap.i, snap.j, snap.k, snap.w);
			frame++;
		}
	}
}