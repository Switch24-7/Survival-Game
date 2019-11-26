using UnityEngine;
using System.Collections;

public class StaticJustinSpawner : MonoBehaviour {

	public AudioClip BossIntro;
	public GameObject enemy;
	public GameObject MinionSpawner;
	private GameObject theEnemy;
	public GameObject left;
	public GameObject right;
	public GameObject middle;
	public GameObject boss;
	public GameObject StaticMinionSpawner;

	private float CDtime = 1.5f;
	private float CD;
	private float BossCDtime = 3f;
	private float BossCD;
	private Transform stephen;
	private bool oneTimeIntro = false;
	private bool oneTimeIntro1 = false;
	bool bossEnabled = false;

	void Start () {
		MinionSpawner.SetActive (false);
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
		Transform stephen = GameObject.Find ("Stephen").GetComponent<Transform> ();
		int totalKills = PlayerMovement.kills;
		if (AllMembersDead()) {
			CD -= Time.deltaTime;
			Spawn ();
		}
		if (totalKills == 15) {
			left.SetActive (true);
			right.SetActive (true);
			middle.SetActive (false);
		}
		if (totalKills == 20) {
			middle.SetActive (true);
			MinionSpawner.SetActive (true);
		}
		if (totalKills == 30) {
			MinionSpawner.SetActive(false);
			StaticMinionSpawner.SetActive (true);
		}
		if (totalKills == 35) {
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