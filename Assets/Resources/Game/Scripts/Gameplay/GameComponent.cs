using UnityEngine;
using System.Collections;

public class GameComponent : MonoBehaviour
{
	protected bool _canPickup = false;
}

public interface IPickupComponent
{
	bool CanPickup { get; set; }
	void PickUp( Player player );
}
public interface ICaptureComponent
{
	float	CaptureTime 	{ get; set; } //n seconds before capture completed
	bool 	Capturing 		{ get; set; }

	void InitializeCapture( Team player );
	void Capture( Team team );
}