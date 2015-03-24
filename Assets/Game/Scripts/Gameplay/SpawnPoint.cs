using UnityEngine;
using System.Collections;

public class SpawnPoint : GameComponent {
	public GameObject spawn; // obj to spawn
	[Header("In seconds.")]
	public float spawnTime; // amount of time between each spawn
	public float spawnDivergence; // amount of time spawn time can vary; allows a random element w/ pattern.

	private float counter;

	void Start() 
	{
		SetCounter();
	}

	void Update() 
	{
		counter -= Time.deltaTime;
		if (counter <= 0) 
		{
			SetCounter();
			Spawn();
		}
	}

	private void Spawn()
	{
		Instantiate(spawn,transform.position,Quaternion.identity);
	}

	private void SetCounter() 
	{ 
		counter = spawnTime + Random.Range(-spawnDivergence,spawnDivergence); 
	}
}
