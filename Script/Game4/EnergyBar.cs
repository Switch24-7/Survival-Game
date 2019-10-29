using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : MonoBehaviour {

	public float totalEnergy = 100f;
	float currentEnergy;

	PlayerMovement4 stephen;

	void Start()
	{
		stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement4> ();
	}
	void Update () {
		currentEnergy = stephen.energy;
		transform.localScale = new Vector3 ((currentEnergy/totalEnergy),1,1);
	}
}
