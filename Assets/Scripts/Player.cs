using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrueSync;

public class Player : TrueSyncBehaviour 
{
	[Header ("Movement")]
	public FP GravityForce = 3;
	public FP MovementSpeed = 5;

	[Header ("Fire")]
	public GameObject bulletPrefab;
	public FP fireCooldown = 0.5f;
	public bool canFire = true;

	[Header ("LookAt")]
	public LayerMask MouseRayLayer;

	protected const byte INPUT_MOVE = 0;

	protected const byte INPUT_FIRE = 2;

	private TSRigidBody rigidBody;

	public override void OnSyncedStart ()
	{
		Spawn ();

		rigidBody = GetComponent<TSRigidBody> ();
	}

	void Spawn ()
	{
		Transform spawn = GameObject.FindGameObjectWithTag ("PlayersSpawns").transform.GetChild (owner.Id - 1);
		TSVector position = new TSVector ((FP)spawn.position.x, (FP)spawn.position.y, (FP)spawn.position.z);
		tsTransform.position = position;
	}

	public override void OnSyncedInput ()
	{
		TSVector movement = new TSVector ((FP) Input.GetAxis ("Horizontal"), 0, (FP) Input.GetAxis ("Vertical"));

		TrueSyncInput.SetTSVector (INPUT_MOVE, movement);

		TrueSyncInput.SetInt (INPUT_FIRE, Input.GetButtonDown ("Fire1") ? 1 : 0);
	}

	public override void OnSyncedUpdate ()
	{
		Movement ();

		Gravity ();

		LookAtMouse ();

		if (TrueSyncInput.GetInt (INPUT_FIRE) == 1 && canFire)
			Fire ();
	}

	void Movement ()
	{
		TSVector movement = TrueSyncInput.GetTSVector (INPUT_MOVE);
		
		TSVector velocity = rigidBody.velocity;
		velocity.y = 0;
		
		rigidBody.AddForce (movement * MovementSpeed - velocity, ForceMode.Impulse);
	}

	void LookAtMouse ()
	{
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;

		// Perform the raycast and if it hits something on the floor layer...
		if (Physics.Raycast(camRay, out floorHit, Mathf.Infinity, MouseRayLayer))
		{
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			TSVector playerToMouse = new TSVector ((FP) floorHit.point.x, (FP) floorHit.point.y, (FP) floorHit.point.z);
			playerToMouse.y = 0f;

			tsTransform.LookAt (playerToMouse);
		}
	}

	void Fire ()
	{
		StartCoroutine (FireCoolDown ());
		
		TSVector position = new TSVector (transform.GetChild (0).position.x, transform.GetChild (0).position.y, transform.GetChild (0).position.z);
		GameObject bullet = TrueSyncManager.SyncedInstantiate (bulletPrefab, position, tsTransform.rotation);
	}

	IEnumerator FireCoolDown ()
	{
		canFire = false;

		yield return fireCooldown;

		canFire = true;
	}

	void Gravity ()
	{
		rigidBody.AddForce (TSVector.down * GravityForce, ForceMode.Force);
	}

	public override void OnPlayerDisconnection (int playerId)
	{
		
	}
}
