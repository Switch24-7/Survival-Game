using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI4 : MonoBehaviour {

	private Text myText;
	private PlayerMovement4 stephen;

	void Start () {

		Reset ();
	}

	void Update () {
		myText = GameObject.Find("Health Amount").GetComponent<Text> ();
		stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement4> ();
		float health = stephen.health;
		myText.text = string.Format ("{0:N0}", health);
	}

	public void Reset()
	{
		myText = GameObject.Find("Health Amount").GetComponent<Text> ();
		myText.text = "100";
	}
}
