using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustinProjectile : MonoBehaviour {

	private Transform thePlayer;
	private Transform transform;
	private PlayerMovement stephen;
	public float damage = 5f;

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.transform.tag == "Environment")
			Destroy (this.gameObject);
		else if ( col.transform.tag == "Floor")
			Destroy(this.gameObject);
		else if (col.transform.tag == "Door")
			Destroy(this.gameObject);
		else if (col.transform.tag == "Player") 
		{
			stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement> ();
			if (stephen.bossIsDead == false) {
				CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
				camera.ShakeCamera (0.1f, 1f);
				stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement> ();
				stephen.health -= damage;
			}
			Destroy (this.gameObject);
		}
	}

}
