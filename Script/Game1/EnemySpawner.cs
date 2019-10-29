using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;
	public GameObject StaticMinionSpawner;
	public GameObject StaticJustinSpawner;
	private GameObject theEnemy;

	int totalKills;
	private float CDtime = .5f;
	private float CD;

	void Start () {
		CD = CDtime;
		Spawn ();
	}
	void Spawn()
	{
		if (CD <= 0) {
			foreach (Transform kid in transform) {
				theEnemy = Instantiate (enemy, kid.transform.position, Quaternion.identity) as GameObject;
				theEnemy.transform.parent = kid; //parent of the "Transform kid"
				CD = CDtime;
			}
		}
	}
	void Update () {
		totalKills = PlayerMovement.kills;
		//totalKills = GameObject.Find ("Stephen").GetComponent<PlayerMovement> ().kills;
		if (AllMembersDead()) {
			CD -= Time.deltaTime;
			Spawn ();
		}
		if (totalKills == 6) {
			StaticMinionSpawner.SetActive (true);
		}
		if (totalKills == 12) {
			StaticJustinSpawner.SetActive (true);
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

	Transform NextFreePosition()
	{
		foreach (Transform Child in transform) 
		{
			if (Child.childCount == 0)
				return Child;
		}
		return null;
	}
}
