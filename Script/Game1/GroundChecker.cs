using UnityEngine;
using System.Collections;

public class GroundChecker : MonoBehaviour {
	private PlayerMovement stephen;

	void Start () {
		stephen = gameObject.GetComponentInParent<PlayerMovement> ();
	}
	void OnTriggerStay2D (Collider2D col)
	{
		stephen.grounded2 = true;	
	}
	void OnTriggerExit2D (Collider2D col)
	{
		stephen.grounded2 = false;
	}
}
