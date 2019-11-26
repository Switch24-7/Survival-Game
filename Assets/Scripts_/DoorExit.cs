using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExit : MonoBehaviour {

	private LevelManager levelManager;

	public void Exit()
	{
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		levelManager.LoadLevel ("Start");
	}
}
