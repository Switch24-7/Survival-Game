using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticJustinSpawner2 : MonoBehaviour {

	public AudioClip BossIntro;
	public GameObject enemy;
	public GameObject MinionSpawner;
	private GameObject theEnemy;
	public GameObject left;
	public GameObject right;
	public GameObject middle;
	public GameObject boss;
	public GameObject StaticMinionSpawner;

	bool bossEnabled = false;
	private float CDtime = 3f;
	private float CD;
	private float BossCDtime = 6f;
	private float BossCD;
	private Transform stephen;
	private bool oneTimeIntro = false;
	private bool oneTimeIntro1 = false;

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
		if (totalKills == (10 + killedFromPrevious)) {
			left.SetActive (true);
			right.SetActive (true);
		}
		if (totalKills == (20 + killedFromPrevious)) {
			GameObject.Find ("BackgroundMusic").GetComponent<BackgroundMusic> ().Pause ();
			oneTimeIntro = true;
			bossEnabled = true;

		} 
		if (oneTimeIntro == true && oneTimeIntro1 == false) 
		{
			AudioSource.PlayClipAtPoint (BossIntro, stephen.position, .66f);
			BossCD = BossCDtime;
			oneTimeIntro = false;
			oneTimeIntro1 = true;
		}
		if (bossEnabled == true)
		{
			if (BossCD < 0)
				boss.SetActive (true);
		}
		BossCD -= Time.deltaTime;
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
