using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : Singleton<GameManager>
{

	public List<GameObject> PlayerList = new List<GameObject> ();

	public Text Score_Text;
	public int Score;
	public GameObject GameOver;
	public Text FinalScore;
	public Text BestScore;

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

	public void AddScore (int value)
	{
		Score += value;
		Score_Text.text = Score.ToString ();
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
			if (PlayerPrefs.GetInt ("Score") != null) {
				if (Score > PlayerPrefs.GetInt ("Score")) {
					PlayerPrefs.SetInt ("Score", Score);
				}
			} else {
				PlayerPrefs.SetInt ("Score", Score);
			}

			FinalScore.text = Score.ToString ();
			BestScore.text = PlayerPrefs.GetInt ("Score").ToString ();
			GameOver.SetActive (true);

			//SceneManager.LoadScene ("Lobby");
			//Application.Quit ();
		}
	}

	public void GoBackToMenu ()
	{
		SceneManager.LoadScene ("Lobby");
	}

}
