using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blocks4 : MonoBehaviour {

	public float health = 500f;
	void Update(){
		if (health <= 0)
		{
			Destroy (gameObject);
		}
	}
	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.transform.tag == "Projectile") {
			health -= 10f;
		}
	}
}
