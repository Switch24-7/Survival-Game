using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerranHealthBarScript : MonoBehaviour {

	public float totalHP = 1000f;
	float currentHP;

	TerranBehavior terran;

	void Start()
	{
		terran = GameObject.Find ("Terran").GetComponent<TerranBehavior> ();
	}
	void Update () {
		currentHP = terran.myHealth;
		transform.localScale = new Vector3 ((currentHP/totalHP),1,1);
	}
}
