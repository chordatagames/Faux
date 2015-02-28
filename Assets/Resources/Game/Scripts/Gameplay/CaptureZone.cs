using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaptureZone : GameComponent, ICaptureComponent
{
	private List<Player> playersInZone 	 = new List<Player> ();
	private List<Player> capturers = new List<Player> ();

	public Team 			capturingTeam;
	public GameComponent 	CaptureObject; //Most cases, this will be a planet

	public int 		startTime 	= 60; //Starting value of Capture Time - 60 = One Min
	public float 	resetSpeed 	= 2; // ResetTick - 2 = 30 sec for reset, 3 = 20 sec;
	
	public bool	activeCapturing 	= false; //active meaning, players in zone, otherwise counting up to 'startTime' again.
	public bool	paused 				= false;

	protected float	_captureTime 	= 0;
	protected bool 	_capturable		= true;

	public float 	CaptureTime	{ get { return _captureTime; } 	set { _captureTime = value; } }
	public bool 	Capturable 	{ get { return _capturable; } 	set { _capturable = value; } }
	public bool 	Capturing 	{ get { return (capturingTeam != null); } } //Inactive capturing

	public Player[] PlayersInZone 	{ get { return playersInZone.ToArray(); } }
	public Player[] Capturers 		{ get { return capturers.ToArray(); } }

	void Start()
	{
		CaptureTime = startTime;
	}

	//TODO - when a 'protector' stays in zone after timer has reset, start capturing. - Unpause if no capturers are left

	void OnTriggerEnter2D(Collider2D other)
	{
		Player playerInZone = other.GetComponent<Player> ();
		if (playerInZone != null) // Valid player
		{
			playersInZone.Add (playerInZone);

			if ( Capturing )
			{
				if (playerInZone.OwnedBy == capturingTeam)
				{
					capturers.Add (playerInZone);
				}
				else
				{
					paused = true;
				}
			}
			else if ( playerInZone.OwnedBy != OwnedBy ) 
			{
				StartCapture (playerInZone.OwnedBy);
			}
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		Player playerInZone = other.GetComponent<Player> ();
		if (playerInZone != null) // Valid player
		{
			playersInZone.Remove (playerInZone);

			if ( Capturing )
			{
				if (playerInZone.OwnedBy == capturingTeam)
				{
					capturers.Remove (playerInZone);
					if (Capturers.Length == 0 )
					{
						//Start looking for other players
						paused = false;
						activeCapturing = false; // no 'capturers' in zone, start reverting capture.
					}
				}
				else // a 'protecting' player is not 'protecting' anymore
				{
					if( Capturers.Length == PlayersInZone.Length ) // All players are capturers
					{
						paused = false; // no 'protectors' in zone, continue capturing.
					}
				}
			}
			else if ( playerInZone.OwnedBy != OwnedBy ) 
			{
				StartCapture (playerInZone.OwnedBy);
			}
		}
	}

	void Update ()
	{
		CaptureTick ();
	}

	public void CaptureTick()
	{
		if (CaptureTime > startTime)
		{
			StopCapture();
		}
		else if (CaptureTime <= 0)
		{
			CompleteCapture();
		}
		if ( Capturing ) // As long as a team is capturing, active or not, controll timer.
		{
			if ( !paused )
			{
				if ( CaptureTime < startTime ) // Only when players of captureTeam is in zone
				{
					CaptureTime -= Capturers.Length * Time.deltaTime; // subtract amount of seconds since last CaptureTick
				}
				if ( activeCapturing ) // Only when players of captureTeam is in zone
				{
					CaptureTime -= Time.deltaTime; // subtract amount of seconds since last CaptureTick
				}
				else // If not actively capturing, start resetting captureTime
				{
					CaptureTime += resetSpeed*Time.deltaTime;
				}
			}
		}
		else if (PlayersInZone.Length > 0)
		{
			//if () PlayersInZone is of same team.
			StartCapture (PlayersInZone[0].OwnedBy); // TODO - must pause if there are others in the field
		}
	}

	public void StartCapture ( Team capTeam )
	{
		Debug.Log ("Initializing Capture!");
		capturingTeam = capTeam;
		activeCapturing = true;
	}


	public void StopCapture ()
	{
		Debug.Log ("Capture Stopped!");
		CaptureTime = startTime;
		capturingTeam = null;
	}

	public void CompleteCapture()
	{
		Debug.Log ( CaptureObject.name + " Captured!");
		OwnedBy = capturingTeam;
		StopCapture ();
	}
}
