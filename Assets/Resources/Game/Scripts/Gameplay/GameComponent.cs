using UnityEngine;
using System.Collections;

public class GameComponent : MonoBehaviour
{
	protected bool _canPickup = false;
	protected bool _capturing = false;
}

public interface PickupComponent
{
	bool canPickup { get; set; }
	void PickUp( Player player );
}
public interface CaptureComponent
{
	bool capturing { get; set; }
	void InitializeCapture( Team player );
	void Capture( Team team );
}