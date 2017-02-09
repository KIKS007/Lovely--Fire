using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

	public List<GameObject> PlayerList = new List<GameObject> ();



	// Use this for initialization
	public void AddPlayerToList (GameObject Player)
	{
		PlayerList.Add (Player);
	}

	public GameObject GetRandomPlayer ()
	{

		int spawnID = Random.Range (0, PlayerList.Count - 1);
		if (PlayerList [spawnID] != null) {
			return PlayerList [spawnID];
			
		} else {
			return this.gameObject;
		}
	}

	public void Died (GameObject me)
	{
		bool onealive = false;
		foreach (GameObject obj in PlayerList) {
			if (obj != null && obj != me) {
				Debug.Log ("Player " + obj.name + " is alive!");
				onealive = true;
			}
		}
		if (!onealive) {
			Debug.Log ("YOU LOOSE");
			Application.Quit ();
		}
	}

}
