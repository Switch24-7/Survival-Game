using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErnaniHealthBarScript : MonoBehaviour {

	public float totalHP;
	float currentHP;

	ErnaniBehavior ernani;

	void Start()
	{
		ernani = GameObject.Find ("Ernani").GetComponent<ErnaniBehavior> ();
	}
	void Update () {
		currentHP = ernani.myHealth;
		transform.localScale = new Vector3 ((currentHP/totalHP),1,1);
	}
}
