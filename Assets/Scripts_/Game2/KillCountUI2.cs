using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCountUI2 : MonoBehaviour {

	private Text myText;
	float kills;

	void Start () {

		Reset ();
	}
		
	void Update () {
		myText = GameObject.Find("Enemy Amount").GetComponent<Text> ();
		kills = PlayerMovement2.kills;
		myText.text = string.Format ("{0:N0}", kills);
	}

	public void Reset()
	{
		myText = GameObject.Find("Enemy Amount").GetComponent<Text> () ;
		myText.text = "0";	
	}
}
