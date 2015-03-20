using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIPlayerTracker : MonoBehaviour {

	public GameObject canvasOwner { get; set; }
	public Canvas canvas { get{return GetComponent<Canvas>();} }
	List<GameObject> arrows = new List<GameObject>();
	// Use this for initialization
	void Start ()
	{
		GameObject arrow;
		foreach(GameObject p in World.OtherPlayers(canvasOwner) )
		{
			arrow = (GameObject)Instantiate(Resources.Load<GameObject> ("Game/Prefabs/UI/Arrow"));
			arrow.GetComponent<RectTransform>().SetParent(GetComponent<RectTransform>(), false);
			arrow.name += p.GetComponent<Player>().playerData.playerID;
			arrow.GetComponent<Arrow>().tracking = p.GetComponent<GameComponent>(); 		// 'Player' extends 'GameComponent'
			arrows.Add( arrow );
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
