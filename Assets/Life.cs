using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrueSync;

public class Life : TrueSyncBehaviour
{

	[SerializeField]
	private int LifePoint;


	public int GetLife ()
	{
		return LifePoint;
	}

	public void ChangeLife (bool positif, int value)
	{
		if (positif) {
			LifePoint += value;
		} else {
			LifePoint -= value;
		}
		CheckLife ();
	}

	private void CheckLife ()
	{
		if (LifePoint <= 0) {
			TrueSyncManager.SyncedDestroy (this.gameObject);
		}
	}

	public void OnSyncedCollisionEnter (TSCollision other)
	{
		if (other.gameObject.CompareTag ("Bullet")) {

			//Debug.Log ("COLLIDING: " + other.gameObject.name);
			ChangeLife (false, other.gameObject.GetComponent <Bullet> ().Damage);
		}
	}


}
