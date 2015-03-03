using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour 
{
	public GameObject tracking{ get; set; }
	public Canvas UI_Canvas{ get; set; }
	
	public float size = 10;

	void Awake ()
	{
		if(!GetComponent<Camera>().orthographic)
		{
			GetComponent<Camera>().orthographic = true;
		}
	}

	void Start () 
	{
		GetComponent<Camera>().orthographicSize = size;
		UI_Canvas = new GameObject(name + "_canvas", typeof(Canvas), typeof(CanvasScaler), typeof(CanvasRenderer), typeof(UIPlayerTracker)).GetComponent<Canvas>();
		UI_Canvas.worldCamera = GetComponent<Camera>();
		UI_Canvas.GetComponent<UIPlayerTracker> ().canvasOwner = tracking;
		UI_Canvas.renderMode = RenderMode.ScreenSpaceCamera;
		UI_Canvas.planeDistance = 1;
		//TODO set up UI elements.
	}
	
	void LateUpdate () 
	{
		if(tracking != null)
		{
			Follow();
		}
	}
	
	void Follow()
	{
		transform.position = new Vector3(tracking.transform.position.x, tracking.transform.position.y, -10);
	}
}