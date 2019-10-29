using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManuelPowerUp3 : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.transform.tag == "Player") {
			PlayerPowerUps3 stephen = GameObject.Find ("Stephen").GetComponent<PlayerPowerUps3> ();
			if (stephen.powerUp == false && stephen.powerUp2 == false) {
				stephen.PowerUp2 ();
				Destroy (gameObject);
			}
		}
	}
}
