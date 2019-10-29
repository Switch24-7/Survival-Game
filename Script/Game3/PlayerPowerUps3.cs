using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUps3 : MonoBehaviour {

	PlayerMovement3 stephen;

	public GameObject head;
	public GameObject ManuelPowerUI;
	public bool powerUp = false;
	public bool powerUp2 = false;
	float timeTime = 10f;
	float timeTime2 = 12.2f; //6.1
	bool paused = false;
	float time;

	void Start () {
	}

	void Update () {
		time -= Time.deltaTime;
		if(paused == false && powerUp2 == true)
		{
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (0.15f, 1f);
			head.SetActive (true);
			PlayerMovement3.shootCoolDown = 0.075f;
			GameObject.Find ("Stephen").GetComponent<PlayerMovement3> ().health += 0.025f;
			PlayerMovement3.movementSpeed = 10f;
			PlayerMovement3.rocketSpeed = 10f;
			PlayerMovement3.descendSpeed = -6f;
			ManuelPowerUI.SetActive (true);
			/*if (time <= 0)
			{
				powerUp2 = false;
				BackgroundMusic music = GameObject.Find ("BackgroundMusic").GetComponent<BackgroundMusic> ();
				music.ManuelDone ();
				ManuelPowerUI.SetActive (false);
				head.SetActive (false);
				GameObject.Find ("Stephen").GetComponent<PlayerMovement3> ().health += 1f;
				PlayerMovement3.shootCoolDown = 0.13f;
				PlayerMovement3.movementSpeed = 7f;
				PlayerMovement3.rocketSpeed = 7f;
				PlayerMovement3.descendSpeed = -3f;
			}
			*/
		}
		if (time <= 0 && powerUp == true) {
			powerUp = false;
			BackgroundMusic music = GameObject.Find ("BackgroundMusic").GetComponent<BackgroundMusic> ();
			music.NathanDone ();
			ManuelPowerUI.SetActive (false);
			head.SetActive (false);
			PlayerMovement3.movementSpeed = 7f;
			PlayerMovement3.rocketSpeed = 7f;
			PlayerMovement3.descendSpeed = -3f;
			PlayerMovement3.shootCoolDown = 0.13f;
		} 

	}
	/*
	public void PowerUp()
	{
	if (powerUp2 == false)
	{
		time = timeTime;
		powerUp = true;
		BackgroundMusic music = GameObject.Find ("BackgroundMusic").GetComponent<BackgroundMusic> ();
		music.Nathan ();
		CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
		camera.ShakeCamera (0.1f, time);
		ManuelPowerUI.SetActive (true);
		head.SetActive (true);
		PlayerMovement3.movementSpeed = 10f;
		PlayerMovement3.rocketSpeed = 10f;
		PlayerMovement3.descendSpeed = -6f;
		PlayerMovement3.shootCoolDown = 0.1f;
		GameObject.Find ("Stephen").GetComponent<PlayerMovement3> ().health += 10f;
		}
	}
	*/
	public void PowerUp2()
	{
		if (powerUp == false) 
		{
			GameObject.Find ("Stephen").GetComponent<PlayerMovement3> ().health += 50f;
			paused = true;
			time = timeTime2;
			powerUp2 = true;
			BackgroundMusic music = GameObject.Find ("BackgroundMusic").GetComponent<BackgroundMusic> ();
			music.Manuel ();
			StartCoroutine (quickPause ());
		}

	}
	IEnumerator quickPause()
	{
		print ("waiting");
		Time.timeScale = 0f;
		float pausedEndTime = Time.realtimeSinceStartup + 6.1f;
		while (Time.realtimeSinceStartup < pausedEndTime) 
		{
			yield return 0;
		}
		Time.timeScale = 1f;
		print ("wait done");
		paused = false;
	}
}
