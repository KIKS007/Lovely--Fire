using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrueSync;

public class Movement : TrueSyncBehaviour
{
	private TSRigidBody rigidBody;
	TSVector targetpos;
	public GameObject Target;

	public override void OnSyncedStart ()
	{
		rigidBody = GetComponent<TSRigidBody> ();
		Target = GameManager.Instance.GetRandomPlayer ();
		//Debug.Log (Target.name);
	}

	public override void OnSyncedUpdate ()
	{
		Move ();

		Gravity ();

		if (Target != null) {
			targetpos = new TSVector (Target.transform.position.x, Target.transform.position.y, Target.transform.position.z);
		} else {
			Target = GameManager.Instance.GetRandomPlayer ();
			targetpos = new TSVector (Target.transform.position.x, Target.transform.position.y, Target.transform.position.z);
		}


		tsTransform.LookAt (targetpos);

	}

	void Move ()
	{
		TSVector movement = new TSVector (this.transform.forward.x, this.transform.forward.y, this.transform.forward.z);

		rigidBody.velocity = movement * 4;

		//rigidBody.AddForce (movement * MovementSpeed - velocity, ForceMode.Impulse);
	}

	void Gravity ()
	{
		rigidBody.AddForce (TSVector.down * 3, ForceMode.Force);
	}
}
