using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker3 : MonoBehaviour {

	private PlayerMovement3 stephen;

	void Start () {
		stephen = gameObject.GetComponentInParent<PlayerMovement3> ();
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
