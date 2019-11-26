using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour {

	float bloodTime = 0.3f;
	float blood;

	// Use this for initialization
	void Start () {
		blood = bloodTime;
	}
	
	// Update is called once per frame
	void Update () {
		blood -= Time.deltaTime;
		if (blood <= 0)
			Destroy (this.gameObject);
	}
}
