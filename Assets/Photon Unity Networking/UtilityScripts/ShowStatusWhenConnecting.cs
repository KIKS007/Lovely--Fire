﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowStatusWhenConnecting : MonoBehaviour 
{
    public GUISkin Skin;
	public Text status;
	public Text connecting;

    void OnGUI()
    {
        if( Skin != null )
        {
            GUI.skin = Skin;
        }

        float width = 400;
        float height = 100;

        Rect centeredRect = new Rect( ( Screen.width - width ) / 2, ( Screen.height - height ) / 2, width, height );

		/*GUILayout.BeginArea( centeredRect, GUI.skin.box );
        {
            GUILayout.Label( "Connecting" + GetConnectingDots(), GUI.skin.customStyles[ 0 ] );
            GUILayout.Label( "Status: " + PhotonNetwork.connectionStateDetailed );
        }
        GUILayout.EndArea();*/

		status.text = "Status: " + PhotonNetwork.connectionStateDetailed.ToString ();
		connecting.text = "Connecting" + GetConnectingDots ();

        if( PhotonNetwork.inRoom )
        {
            enabled = false;
        }
    }

    string GetConnectingDots()
    {
        string str = "";
        int numberOfDots = Mathf.FloorToInt( Time.timeSinceLevelLoad * 3f % 4 );

        for( int i = 0; i < numberOfDots; ++i )
        {
            str += " .";
        }

        return str;
    }
}
