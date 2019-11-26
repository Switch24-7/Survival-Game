using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NathanPowerUp2 : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.transform.tag == "Player") {
			PlayerPowerUps2 stephen = GameObject.Find ("Stephen").GetComponent<PlayerPowerUps2> ();
			stephen.PowerUp ();
			Destroy (gameObject);
		}
	}
}
