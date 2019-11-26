using UnityEngine;
using System.Collections;

public class EnemySpawner2 : MonoBehaviour {

	public GameObject enemy;
	public GameObject StaticMinionSpawner;
	public GameObject StaticJustinSpawner;
	private GameObject theEnemy;

	int totalKills;
	private float CDtime = 1f;
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
		int killedFromPrevious = PlayerMovement.kills;
		int totalKills = PlayerMovement2.kills;
		if (AllMembersDead()) {
			CD -= Time.deltaTime;
			Spawn ();
		}
		if (totalKills == (killedFromPrevious + 10)) {
			StaticMinionSpawner.SetActive (true);
		}
		if (totalKills == (killedFromPrevious + 20)) 
		{
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
}
