using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	private static bool paused = false;
	public GameObject pauseMenu;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			
			paused = togglePause ();
		} 
		
	}
	bool togglePause()
	{
		if (Time.timeScale == 0f) {
			Time.timeScale = 1f;
			pauseMenu.SetActive (false);
			return(false);
		} 
		else 
		{
			Time.timeScale = 0f;
			pauseMenu.SetActive (true);
			return(true);
		}
	}
}
