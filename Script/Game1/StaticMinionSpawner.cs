using UnityEngine;
using System.Collections;

public class StaticMinionSpawner : MonoBehaviour {

	public GameObject enemy;
	private GameObject theEnemy;

	private Transform child;
	private float CDtime = 2f;
	private float CD;

	void Start () {
		CD = CDtime;
		Spawn ();
	}

	void Update () {
		if (AllMembersDead()) {
			CD -= Time.deltaTime;
			Spawn ();
		}
	}

	void Spawn()
	{
		if (CD <= 0) {
			foreach (Transform kid in transform) {
				child = kid;
				theEnemy = Instantiate (enemy, kid.transform.position, Quaternion.identity) as GameObject;
				theEnemy.transform.parent = kid; //parent of the "Transform kid"
				CD = CDtime;
			}
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
