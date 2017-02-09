using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrueSync;

public class BadGuySpawner : TrueSyncBehaviour
{
	public float timeBetweenSpawn = 5;

	[SerializeField]
	GameObject BadGuy;

	[SerializeField]
	GameObject[] Spawns;

	private int spawnCount = 0;

	public int spawnID = 0;

	bool SpawnGuy = true;

	// Use this for initialization
	public override void OnSyncedStart ()
	{
		
	}
	
	// Update is called once per frame
	public override void OnSyncedUpdate ()
	{
		if (SpawnGuy) 
		{
			SpawnGuy = false;
			TrueSyncManager.SyncedStartCoroutine (SpawnTimer ());
		}
	}

	public void SpawnBadGuy ()
	{
		TrueSyncManager.SyncedInstantiate (BadGuy, Spawns [spawnID].GetComponent <TSTransform> ().position, TSQuaternion.identity);

		spawnID++;

		if (spawnID > Spawns.Length - 1)
			spawnID = 0;
	}

	IEnumerator SpawnTimer ()
	{
		SpawnBadGuy ();

		yield return timeBetweenSpawn;

		if(spawnCount >= 5 && timeBetweenSpawn >= 2)
		{
			timeBetweenSpawn -= 2;
			spawnCount = 0;
		}

		SpawnGuy = true;
	}
}
