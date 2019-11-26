using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaiHealthScript : MonoBehaviour {

	public float totalHP = 3000f;
	float currentHP;

	SaiBehavior sai;

	void Start()
	{
		sai = GameObject.Find ("Sai").GetComponent<SaiBehavior> ();
	}
	void Update () {
		currentHP = sai.myHealth;
		transform.localScale = new Vector3 ((currentHP/totalHP),1,1);
	}
}
