﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErnaniAppear : MonoBehaviour {

	float timeTime = 0.5f;
	float time;
	// Use this for initialization
	void Start () {
		time = timeTime;
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if (time <= 0)
			Destroy (this.gameObject);
	}
}
