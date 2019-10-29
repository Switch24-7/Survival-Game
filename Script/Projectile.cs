using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float damage = 10f;
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.transform.tag == "Environment")
			Destroy (this.gameObject);
		if (col.transform.tag == "Floor")
			Destroy (this.gameObject);
		else if (col.transform.tag == "Enemy") 
		{
			Destroy (this.gameObject);
		}
	}
}
