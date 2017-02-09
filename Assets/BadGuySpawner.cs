using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrueSync;


public class BadGuySpawner : TrueSyncBehaviour
{
	[SerializeField]
	GameObject BadGuy;

	[SerializeField]
	GameObject[] Spawns;

	bool SpawnGuy = true;
	// Use this for initialization
	public override void OnSyncedStart ()
	{
		StartCoroutine ("SpawnTimer");
	}
	
	// Update is called once per frame
	public override void OnSyncedUpdate ()
	{
		if (SpawnGuy) {
			SpawnGuy = false;
			SpawnBadGuy ();
			StartCoroutine ("SpawnTimer");
		}
	}

	public void SpawnBadGuy ()
	{
		int spawnID = Random.Range (0, Spawns.Length);
		GameObject bullet = TrueSyncManager.SyncedInstantiate (BadGuy, Spawns [spawnID].GetComponent <TSTransform> ().position, Spawns [spawnID].GetComponent <TSTransform> ().rotation);

	}

	IEnumerator SpawnTimer ()
	{
		yield return new WaitForSeconds (2);
		SpawnGuy = true;
	}
}
