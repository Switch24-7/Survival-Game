using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour {

	float timeTime = 1f;
	float time;

	void Start () {
		time = timeTime;
	}
	

	void Update () {
		time -= Time.deltaTime;
		if (time <= 0) 
		{
			Destroy (this.gameObject);
		}
	}
}
