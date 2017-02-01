using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrueSync;

public class Player : TrueSyncBehaviour 
{
	[Header ("Movement")]
	public FP movementSpeed;

	protected const byte INPUT_MOVE = 0;
	protected const byte INPUT_AIM = 1;

	protected const byte INPUT_FIRE = 2;

	private TSRigidBody rigidBody;

	public override void OnSyncedStart ()
	{
		rigidBody = GetComponent<TSRigidBody> ();
	}

	public override void OnSyncedInput ()
	{
		TSVector movement = new TSVector ((FP) Input.GetAxis ("Horizontal"), 0, (FP) Input.GetAxis ("Vertical"));

		TrueSyncInput.SetTSVector (INPUT_MOVE, movement);
	}

	public override void OnSyncedUpdate ()
	{
		Debug.Log (TrueSyncInput.GetTSVector (INPUT_MOVE));

		TSVector movement = TrueSyncInput.GetTSVector (INPUT_MOVE);
		
		rigidBody.AddForce (movement * movementSpeed - rigidBody.velocity, ForceMode.Impulse);

		Movement ();
	}

	void Movement ()
	{
	}

	public override void OnPlayerDisconnection (int playerId)
	{
		
	}
}
