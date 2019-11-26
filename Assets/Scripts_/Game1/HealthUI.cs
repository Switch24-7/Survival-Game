using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

	private Text myText;
	private PlayerMovement stephen;

	void Start () {
		
		Reset ();
	}

	void Update () {
		myText = GameObject.Find("Health Amount").GetComponent<Text> ();
		stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement> ();
		float health = stephen.health;
		myText.text = string.Format ("{0:N0}", health);
	}

	public void Reset()
	{
		myText = GameObject.Find("Health Amount").GetComponent<Text> ();
		myText.text = "100";
	}
}
