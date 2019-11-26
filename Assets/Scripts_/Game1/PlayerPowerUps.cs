using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour {

	PlayerMovement stephen;
	public GameObject head;
	public GameObject NathanPowerUI;
	public GameObject snoopDogg1;
	public GameObject snoopDogg2;

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
			PlayerMovement.movementSpeed = 7f;
			PlayerMovement.shootCoolDown = 0.2f;
			snoopDogg1.SetActive (false);
			snoopDogg2.SetActive (false);
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
		PlayerMovement.movementSpeed = 10f;
		PlayerMovement.shootCoolDown = 0.1f;
		GameObject.Find ("Stephen").GetComponent<PlayerMovement> ().health += 5f;
	}
}

