using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrueSync;

public class Bullet : TrueSyncBehaviour
{
	[Header ("Bullet")]
	public FP bulletSpeed = 2;

	public int Damage = 5;

	private TSRigidBody rigidBody;

	public override void OnSyncedStart ()
	{
		rigidBody = GetComponent<TSRigidBody> ();

		rigidBody.AddForce (tsTransform.forward * bulletSpeed, ForceMode.Impulse);
	}

	public override void OnSyncedUpdate ()
	{
		TSVector velocity = rigidBody.velocity;
		velocity.y = 0;

		//rigidBody.AddForce (tsTransform.forward * bulletSpeed - velocity, ForceMode.Impulse);
	}

	void OnBecameInvisible ()
	{
		TrueSyncManager.SyncedDestroy (gameObject);
	}
}
