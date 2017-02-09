using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Photon;
using UnityEngine.UI;

/// <summary>
/// This script automatically connects to Photon (using the settings file),
/// tries to join a random room and creates one if none was found (which is ok).
/// </summary>
public class JoinRandom : PunBehaviour
{
	[Header ("Lobby")]
	public string lobbyName = "Lobby";
	public string levelToLoadPath = "";

	[Header ("UI")]
	public GameObject shootButton;
	public GameObject connecting;
	public Text playersText;

	public virtual void Start()
	{
		PhotonNetwork.automaticallySyncScene = true;
		PhotonNetwork.CrcCheckEnabled = true;
		PhotonNetwork.ConnectUsingSettings("v1.0");
	}

	public override void OnJoinedLobby ()
	{
		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.MaxPlayers = 0;

		PhotonNetwork.JoinOrCreateRoom ("room1", roomOptions, null);
	}

	void OnGUI() {
		//GUI.Label (new Rect(10, 10, 100, 30), "players: " + PhotonNetwork.playerList.Length);

		playersText.text = PhotonNetwork.playerList.Length.ToString ();

		if (PhotonNetwork.isMasterClient && !shootButton.activeSelf)
		{
			shootButton.SetActive (true);
			connecting.SetActive (false);			
		}
	}

	public void Launch ()
	{
		if (PhotonNetwork.isMasterClient) 
		{
			PhotonNetwork.LoadLevel (levelToLoadPath);
		}
	}

	public void Quit ()
	{
		Application.Quit ();
	}
}