using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUps2 : MonoBehaviour {
	
	PlayerMovement2 stephen;

	public GameObject head;
	public GameObject snoopDogg1;
	public GameObject snoopDogg2;
	public GameObject NathanPowerUI;
	bool powerUp = false;
	float timeTime = 10f;
	float time;

	void Start () {
	}

	void Update () {
		time -= Time.deltaTime;
		if (time <= 0 && powerUp == true) 
		{
			powerUp = false;
			BackgroundMusic music = GameObject.Find ("BackgroundMusic").GetComponent<BackgroundMusic> ();
			music.NathanDone ();
			NathanPowerUI.SetActive (false);
			head.SetActive (false);
			snoopDogg1.SetActive (false);
			snoopDogg2.SetActive (false);
			PlayerMovement2.movementSpeed = 7f;
			PlayerMovement2.rocketSpeed = 7f;
			PlayerMovement2.descendSpeed = -3f;
			PlayerMovement2.shootCoolDown = 0.2f;
		}

	}

	public void PowerUp()
	{
		time = timeTime;
		powerUp = true;
		BackgroundMusic music = GameObject.Find ("BackgroundMusic").GetComponent<BackgroundMusic> ();
		music.Nathan ();
		CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
		camera.ShakeCamera (0.1f, time);
		NathanPowerUI.SetActive (true);
		head.SetActive (true);
		snoopDogg1.SetActive (true);
		snoopDogg2.SetActive (true);
		PlayerMovement2.movementSpeed = 10f;
		PlayerMovement2.rocketSpeed = 10f;
		PlayerMovement2.descendSpeed = -6f;
		PlayerMovement2.shootCoolDown = 0.1f;
		GameObject.Find ("Stephen").GetComponent<PlayerMovement2> ().health += 5f;
	}
}
