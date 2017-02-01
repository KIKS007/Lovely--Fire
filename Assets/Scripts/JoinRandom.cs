using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Photon;

/// <summary>
/// This script automatically connects to Photon (using the settings file),
/// tries to join a random room and creates one if none was found (which is ok).
/// </summary>
public class JoinRandom : PunBehaviour
{
	[Header ("Lobby")]
	public string lobbyName = "Lobby";
	public string levelToLoadPath = "";


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
		GUI.Label (new Rect(10, 10, 100, 30), "players: " + PhotonNetwork.playerList.Length);

		if (PhotonNetwork.isMasterClient && GUI.Button (new Rect (10, 40, 100, 30), "start")) 
		{
			PhotonNetwork.LoadLevel (levelToLoadPath);
		}
	}
}