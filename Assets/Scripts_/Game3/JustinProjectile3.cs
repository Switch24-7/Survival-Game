using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustinProjectile3 : MonoBehaviour {

	private Transform thePlayer;
	private Transform transform;
	private PlayerMovement3 stephen;
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
			stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement3> ();
			if (stephen.bossIsDead == false) {
				CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
				camera.ShakeCamera (0.25f, 1f);
				stephen.health -= damage;
			}
			Destroy (this.gameObject);
		}
	}
}
