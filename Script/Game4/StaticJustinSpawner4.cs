using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticJustinSpawner4 : MonoBehaviour {

	//public AudioClip BossIntro;
	public GameObject theEnemy;

	bool bossEnabled = false;
	private float CDtime = 1f;
	private float CD;
	private Transform stephen;

	void Awake()
	{
		CD = CDtime;
		Spawn ();
	}

	void Start () {
		stephen = GameObject.Find ("Stephen").GetComponent<Transform> ();
	}
	void Spawn()
	{
		if (CD <= 0) {
			foreach (Transform kid in transform) {
				GameObject newEnemy = Instantiate (theEnemy, kid.transform.position, Quaternion.identity) as GameObject;
				newEnemy.transform.parent = kid; //parent of the "Transform kid"
				CD = CDtime;
			}
		}
	}
	void Update () {
		int killedFromPrevious = PlayerMovement2.kills;
		int totalKills = PlayerMovement3.kills;
		if (AllMembersDead ()) {
			CD -= Time.deltaTime;
			Spawn ();
		}
	}
	bool AllMembersDead()
	{
		foreach (Transform Child in transform) 
		{
			if (Child.childCount > 0)
				return false;
		}
		return true;
	}
}
