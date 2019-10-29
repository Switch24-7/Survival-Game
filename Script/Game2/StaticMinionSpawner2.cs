using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMinionSpawner2 : MonoBehaviour {
	
public GameObject enemy;
private GameObject theEnemy;

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
void Update () 
	{
		int killedFromPrevious = PlayerMovement.kills;
		int totalKills = PlayerMovement2.kills;
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
