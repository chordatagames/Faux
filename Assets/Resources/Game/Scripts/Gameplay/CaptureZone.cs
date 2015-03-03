using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaptureZone : GameComponent, ICaptureComponent
{
	private List<GameObject> 	objectsInZone	= new List<GameObject> ();
	private List<Player> 		playersInZone	= new List<Player> ();
	private List<Player> 		capturersInZone = new List<Player> ();

	private Team capturingTeam = null;
	
	public GameComponent 	CaptureObject; //Most cases, this will be a planet

	public int 		startTime 	= 60; //Starting value of Capture Time - 60 = One Min
	public float 	resetSpeed 	= 2; // ResetTick - 2 = 30 sec for reset, 3 = 20 sec;

	protected float	_captureTime 	= 0;
	protected bool 	_capturable		= true;

	public float 	CaptureTime	{ get { return _captureTime; } 	set { _captureTime = value; } }
	public bool 	Capturable 	{ get { return _capturable; } 	set { _capturable = value; } }

	public bool 	Capturing 	{ get { return (_captureTime < startTime); } } //Inactive capturing

	public GameObject[] ObjectsInZone	{ get { return objectsInZone.ToArray(); } }
	public Player[] 	PlayersInZone 	{ get { return playersInZone.ToArray(); } }
	public Player[] 	CapturersInZone { get { return capturersInZone.ToArray(); } }

	void Start()
	{
		CaptureTime = startTime;
	}

	//TODO - when a 'protector' stays in zone after timer has reset, start capturing. - Unpause if no capturers are left

	void OnTriggerEnter2D(Collider2D other)
	{
		objectsInZone.Add(other.gameObject);
		Player player = other.GetComponent<Player> ();
		if ( player != null ) // player is valid;
		{
			playersInZone.Add ( player );

			if (capturingTeam == null && !(player.OwnedBy == OwnedBy))
			{
				StartCapture( player.OwnedBy );
			}

			if ( player.OwnedBy == capturingTeam ) // the player is a capturer
			{
				capturersInZone.Add ( player );
			}
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		objectsInZone.Remove(other.gameObject);
		Player player = other.GetComponent<Player> ();
		if ( player != null ) // player is valid;
		{
			playersInZone.Remove ( player );
			
			if ( player.OwnedBy == capturingTeam ) // the team of the player is not already owner
			{
				capturersInZone.Remove ( player );
			
			}
		}
	}

	void Update ()
	{
		ValidatePlayersInZone();

		CaptureUpdate ();
	}
	void ValidatePlayersInZone()
	{
		foreach ( Player p in PlayersInZone )
		{
			if ( p.dead )
			{
				playersInZone.Remove ( p );
				if ( p.OwnedBy == capturingTeam)
				{
					capturersInZone.Remove ( p );
				}
			}
		}
	}

	void CaptureUpdate()
	{
		if (capturingTeam != null)
		{
			if ( CapturersInZone.Length > 0)
			{
				if ( CapturersInZone.Length == PlayersInZone.Length ) //Otherwise, don't count the timer, capture is paused
				{
					if (CaptureTime < 0)
					{
						CompleteCapture();
					}
					else
					{
						CaptureTime-=Time.deltaTime*CapturersInZone.Length;
					}
				}
			}
			else
			{
				if (CaptureTime > startTime)
				{
					StopCapture();
				}
				else
				{
					CaptureTime+=Time.deltaTime*resetSpeed;
				}
			}
		}
	}

	public void StartCapture ( Team capturerTeam )
	{
		capturingTeam = capturerTeam;
	}


	public void StopCapture ()
	{
		CaptureTime = startTime;
		capturingTeam = null;
	}

	public void CompleteCapture()
	{
		OwnedBy = capturingTeam;
		StopCapture ();
	}
}
