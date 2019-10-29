using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour {

	public GameObject explosion;
	private Transform thePlayer;
	private Transform transform;
	private PlayerMovement4 stephen;
	public float damage = 5f;

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.transform.tag == "Environment")
			Destroy (this.gameObject);
		else if ( col.transform.tag == "Floor")
			Destroy(this.gameObject);
		else if (col.transform.tag == "Door")
			Destroy(this.gameObject);
		else if (col.transform.tag == "Player") 
		{
			GameObject explode = Instantiate(explosion, col.transform.position,Quaternion.identity);
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (0.25f, 1f);
			stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement4> ();
			stephen.health -= damage;
			stephen.energy += 10f;
			Destroy (this.gameObject);
		}
	}
}
