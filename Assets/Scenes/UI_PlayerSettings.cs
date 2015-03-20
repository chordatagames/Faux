using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_PlayerSettings : MonoBehaviour
{
	Color playerColor;
	public Slider R;
	public Slider G;
	public Slider B;

	void Start()
	{
		UpdatePlayerColor ();
	}

	public void UpdatePlayerColor()
	{
		playerColor = new Color (R.value, G.value, B.value);
		GetComponent<Image>().material.SetColor("_Color", playerColor);
	}
}
