using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NathanPowerUp : MonoBehaviour {
	
	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.transform.tag == "Player") {
			PlayerPowerUps stephen = GameObject.Find ("Stephen").GetComponent<PlayerPowerUps> ();
			stephen.PowerUp ();
			Destroy (gameObject);
		}
	}
}