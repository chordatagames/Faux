
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Arrow : MonoBehaviour
{
	Rect bounds;
	RectTransform rTrans;
	Camera canvasCam;
	public GameObject tracking { get; set; }
	public float margin = 5;
	// Use this for initialization
	void Start () 
	{
		rTrans = GetComponent<RectTransform> ();
		canvasCam = GetComponentInParent<Canvas> ().worldCamera;

	}
	
	// Update is called once per frame
	void Update () 
	{

		Vector3 trackPos = tracking.transform.position - canvasCam.transform.position;
		if(trackPos.x < canvasCam.orthographicSize * -canvasCam.aspect - margin || trackPos.x > canvasCam.orthographicSize * canvasCam.aspect + margin || 
		   trackPos.y < -canvasCam.orthographicSize - margin || trackPos.y > canvasCam.orthographicSize + margin)
		{
			GetComponent<CanvasRenderer>().SetAlpha(1);
			MoveArrow();
		}
		else
		{
			GetComponent<CanvasRenderer>().SetAlpha(0);
		}
	}

	void MoveArrow()
	{
		Vector3 dir = (tracking.transform.position - rTrans.position).normalized;
		float angle = 270 + Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg ;
		rTrans.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		float max = Mathf.Max (Mathf.Abs (dir.x), Mathf.Abs (dir.y * canvasCam.aspect));

		rTrans.localPosition = new Vector3 (dir.x / max, dir.y / max) * canvasCam.pixelHeight;

		Debug.Log (rTrans.anchoredPosition);
	}
}
