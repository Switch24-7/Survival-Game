using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrickHealthScript : MonoBehaviour {

	public float totalHP = 10000f;
	float currentHP;

	PatrickBehavior patrick;

	void Start()
	{
		patrick = GameObject.Find ("Patrick").GetComponent<PatrickBehavior> ();
	}
	void Update () {
		currentHP = patrick.myHealth;
		transform.localScale = new Vector3 ((currentHP/totalHP),1,1);
	}
}
