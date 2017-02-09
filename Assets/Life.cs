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
			if (this.gameObject.CompareTag ("Player")) {

				GameManager.Instance.Died (this.gameObject);
			}
			TrueSyncManager.SyncedDestroy (this.gameObject);

		}
	}

	public void OnSyncedCollisionEnter (TSCollision other)
	{
		if (other.gameObject.CompareTag ("Bullet")) {

			//Debug.Log ("COLLIDING: " + other.gameObject.name);
			ChangeLife (false, other.gameObject.GetComponent <Bullet> ().Damage);
		}
		if (other.gameObject.CompareTag ("BadGuy") && this.gameObject.CompareTag ("Player")) {

			//Debug.Log ("COLLIDING: " + other.gameObject.name);
			ChangeLife (false, 5);
		}
	}


}
