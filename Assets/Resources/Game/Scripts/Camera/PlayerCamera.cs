using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour 
{
	public GameObject tracking{ get; set; }
	public Canvas UI_Canvas{ get; set; }
	
	public float size = 8;

	void Awake ()
	{
		if(!camera.orthographic)
		{
			camera.orthographic = true;
		}
	}

	void Start () 
	{
		camera.orthographicSize = size;
		UI_Canvas = new GameObject(name + "_canvas", typeof(Canvas), typeof(CanvasScaler), typeof(CanvasRenderer), typeof(UIPlayerTracker)).GetComponent<Canvas>();
		UI_Canvas.worldCamera = camera;
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