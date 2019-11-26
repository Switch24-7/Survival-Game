using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningProjectile : MonoBehaviour {
	public float speed = 1200f;
	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
		this.transform.RotateAround (new Vector3 (transform.position.x, transform.position.y, transform.position.z), new Vector3 (0f, 0f, 1f), speed * Time.deltaTime);
	}
}
