using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichardHealthBarScript : MonoBehaviour {

	public float totalHP = 3000f;
	float currentHP;

	RichardBehavior richard;

	void Start()
	{
		richard = GameObject.Find ("Richard").GetComponent<RichardBehavior> ();
	}
	void Update () {
		currentHP = richard.myHealth;
		transform.localScale = new Vector3 ((currentHP/totalHP),1,1);
	}
}
