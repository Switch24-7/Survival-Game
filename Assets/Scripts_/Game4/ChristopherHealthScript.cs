using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristopherHealthScript : MonoBehaviour {

	public float totalHP = 3000f;
	float currentHP;

	ChristopherBehavior christopher;

	void Start()
	{
		christopher = GameObject.Find ("Christopher").GetComponent<ChristopherBehavior> ();
	}
	void Update () {
		currentHP = christopher.myHealth;
		transform.localScale = new Vector3 ((currentHP/totalHP),1,1);
	}
}
