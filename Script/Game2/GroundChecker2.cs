using UnityEngine;
using System.Collections;

public class GroundChecker2 : MonoBehaviour {
	private PlayerMovement2 stephen;

	void Start () {
		stephen = gameObject.GetComponentInParent<PlayerMovement2> ();
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
