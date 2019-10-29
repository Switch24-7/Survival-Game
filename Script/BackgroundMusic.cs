using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

	public AudioSource music;
	public AudioSource music2;
	public AudioSource power;
	public AudioSource power2;
	public AudioSource manuelTheme;
	public bool bossTime = false;
	void Start () {
		music.Play ();
	}

	void Update()
	{
		if (bossTime == true)
			ManuelDone ();
	}

	public void Boss()
	{
		bossTime = true;
		music.Pause ();
		music2.Play ();
	}
	public void Pause()
	{
		music.Pause ();
		music2.Pause ();
		power.Pause ();
		power2.Pause ();
		manuelTheme.Pause ();
	}
	public void Nathan()
	{
		if (bossTime == false)
			power.Play ();
	}
	public void Manuel()
	{
		if (bossTime == false) {
			power2.Play ();
			StartCoroutine (wait ());
		}
	}
	public void NathanDone()
	{
		power.Stop ();
	}
	public void ManuelDone()
	{
		manuelTheme.Stop ();
	}
	IEnumerator wait()
	{
		float pausedEndTime = Time.realtimeSinceStartup + 18f;
		while (Time.realtimeSinceStartup < pausedEndTime) 
		{
			yield return 0;
		}
		manuelTheme.Play ();
	}
}
