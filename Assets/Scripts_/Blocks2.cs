using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks2 : MonoBehaviour {

	public GameObject block3;
	public float health = 500f;
	void Update(){
		if (health <= 0)
		{
			GameObject thirdBlock = Instantiate (block3, transform.position, Quaternion.identity);
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
