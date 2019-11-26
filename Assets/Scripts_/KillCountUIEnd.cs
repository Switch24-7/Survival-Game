using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCountUIEnd : MonoBehaviour {

	private Text myText;
	float kills;

	void Start () {

		Reset ();

	}


	void Update () {
		myText = GameObject.Find("Kill Count number").GetComponent<Text> ();
		kills = PlayerMovement4.kills;
		myText.text = string.Format ("{0:N0}", kills);
	}

	public void Reset()
	{
		myText = GameObject.Find("Kill Count number").GetComponent<Text> () ;
		myText.text = "0";	
	}
}
