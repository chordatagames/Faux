using UnityEngine;
using System.Collections;

//TODO, why the fuck do we use Interfaces with properties anyways!???!?!?

public abstract class GameComponent : MonoBehaviour
{
	public bool onlyOwnerUse = false; //Invalid for some?
	public Team OwnedBy { get; set; }
}
public interface IPickupComponent
{
	bool CanPickUp 	{ get; set; }
	bool PickedUp 	{ get; } // HeldBy != null
	Player HeldBy 	{ get; set; }
	void PickUp( Player player );
}
public interface ICaptureComponent
{
	float	CaptureTime 	{ get; set; } //n seconds before capture completed
	bool	Capturable		{ get; set; }
	bool 	Capturing 		{ get; }
	
	void StartCapture( Team capturerTeam);
	void StopCapture();
	void CompleteCapture();
}
public interface IAlignable
{
	float alignRadius { get; set; }
	void AlignTo(Collider2D body);
}