using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile4 : MonoBehaviour {

	public float damage = 10f;
	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.transform.tag == "Environment")
			Destroy (this.gameObject);
		if (col.transform.tag == "Floor")
			Destroy (this.gameObject);
		else if (col.transform.tag == "Enemy") 
		{
			PlayerMovement4 stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement4> ();
			stephen.energy += 0.25f;
			Destroy (this.gameObject);
		}
	}
}
