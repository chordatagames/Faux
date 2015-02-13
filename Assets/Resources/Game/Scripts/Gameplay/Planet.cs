using UnityEngine;
using System.Collections;

public class Planet : GameComponent, ICaptureComponent
{
	protected float	_captureTime 	= 0;
	protected bool 	_capturing 		= false;

	public int startTime = 60; //Starting value of Capture Time - 60 = One Min
	public float resetSpeed = 2; //Starting value of Capture Time - 60 = One Min
	
	public bool Capturing 	{ get{ return _capturing; } set {_capturing = value;} }
	public float CaptureTime	{ get{ return _captureTime; } set {_captureTime = value;} }

	public void InitializeCapture( Team player )
	{
		Capturing = true;
		CaptureTime = startTime;
		StartCoroutine ( CaptureRoutine( 0.2f ) );
	}
	
	protected virtual IEnumerator CaptureRoutine(float interval)
	{
		while (Capturing || CaptureTime > 0)
		{
			if(CaptureTime <= 0)
			{
//				Capture();
			}
			
			if (Capturing)
			{
				CaptureTime -= interval;
			}
			else
			{
				if(CaptureTime < startTime)
				{
					CaptureTime += resetSpeed*interval;
				}
			}

			yield return new WaitForSeconds(interval);
		}
	}

	public void Capture( Team team )
	{

	}
}
