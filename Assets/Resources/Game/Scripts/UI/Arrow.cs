using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Arrow : MonoBehaviour
{
	Rect bounds;
	RectTransform rTrans;
	Camera canvasCam;
	public GameComponent tracking { get; set; }
	public float margin;

	Vector2 relPos;
	// Use this for initialization
	void Start () 
	{

		margin = 35;
		rTrans = GetComponent<RectTransform> ();
		GetComponent<Image> ().color = tracking.OwnedBy.teamColor;
		canvasCam = GetComponentInParent<Canvas> ().worldCamera;
	}

	// Update is called once per frame
	void Update ()
	{
		Vector3 relPos = tracking.transform.position - canvasCam.transform.position;
		Vector2 edge = new Vector2 (canvasCam.pixelWidth - margin, canvasCam.pixelHeight - margin);
		if (Mathf.Abs (relPos.x / relPos.y) >= Mathf.Abs (edge.x / edge.y)) //Checks whether arrow should be on the right/left (true) or top/bottom (false)
		{
			float x_ = Mathf.Sign(relPos.x);
			float y_ = Mathf.Sign(relPos.x) * relPos.y / relPos.x;
			rTrans.localPosition = new Vector2 (x_, y_) * edge.x / 2;
		}
		else
		{
			float y_ = Mathf.Sign(relPos.y);
			float x_ = Mathf.Sign(relPos.y) * relPos.x / relPos.y;
			rTrans.localPosition = new Vector2 (x_, y_) * edge.y / 2;
		}
		
		if ((Mathf.Abs(relPos.x) < (canvasCam.orthographicSize * canvasCam.aspect)) && (Mathf.Abs(relPos.y) < canvasCam.orthographicSize))
			GetComponent<CanvasRenderer>().SetAlpha(0);
		else
			GetComponent<CanvasRenderer>().SetAlpha(1);

		DoArrowRotation ();

	}

	void DoArrowRotation ()
	{
		Vector3 dir = (tracking.transform.position - rTrans.position).normalized;
		float angle = 270 + Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		rTrans.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
