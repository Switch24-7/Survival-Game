using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {

	public GameObject block2;
	public float health = 500f;
	void Update(){
		if (health <= 0)
		{
			GameObject secondBlock = Instantiate (block2, transform.position, Quaternion.identity);
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
