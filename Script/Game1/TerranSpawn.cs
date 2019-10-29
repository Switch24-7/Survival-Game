using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerranSpawn : MonoBehaviour {

	public GameObject justinSpawner;
	public GameObject enemy;
	private GameObject theEnemy;

	public int childAmount;
	private float CDtime = 10f;
	private float CD;

	void Start () {
		justinSpawner.SetActive (false);
		childAmount = 0;
		CD = CDtime;
	}
	void Spawn()
	{
		if (CD <= 0) {
			foreach (Transform kid in transform) {
				childAmount++;
				theEnemy = Instantiate (enemy, kid.transform.position, Quaternion.identity) as GameObject;
				theEnemy.transform.parent = kid;
			}
		}
	}
	void Update () {
		CD -= Time.deltaTime;
		if (childAmount == 0)
		Spawn ();
	}
	void LateUpdate()
	{
		//int totalKills = GameObject.Find ("Stephen").GetComponent<PlayerMovement> ().kills;
		int totalKills = PlayerMovement.kills;
		if (totalKills == 51) {
			LevelManager levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
			levelManager.LoadLevel ("Game2");
		}
	}
}
