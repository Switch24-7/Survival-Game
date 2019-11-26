using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristianHealthBarScript : MonoBehaviour {

	public float totalHP = 3000f;
	float currentHP;

	ChristianBehavior christian;

	void Start()
	{
		christian = GameObject.Find ("Christian").GetComponent<ChristianBehavior> ();
	}
	void Update () {
		currentHP = christian.myHealth;
		transform.localScale = new Vector3 ((currentHP/totalHP),1,1);
	}
}
