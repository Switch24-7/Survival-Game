﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErnaniBehavior : MonoBehaviour {
	
	public GameObject ErnaniAppear;
	public GameObject ExplosiveEffect;
	public AudioClip hurt;
	public AudioClip death;
	public AudioClip laser;
	public GameObject StaticJustinSpawner;
	public GameObject healthBar;

	private EnemySpawner enemySpawner;
	private SpriteRenderer mySpriteRenderer;
	private Transform stephenTransform;
	private Projectile missile;
	private Animator anim;

	public GameObject projectile;
	public float myHealth = 1000f; //Ernani's health
	public bool dead = false;
	public float speed;
	public float shootCDTime = 1f;
	private float shootCD;
	private bool movingLeft = true;

	bool once = false;
	bool twice = false;
	bool thrice = false;
	bool quadruple = false;
	bool five = false;
	bool last = false;

	float timeTime = 0.9f;
	float time;

	void Awake()
	{
		healthBar.SetActive (true);
		CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
		camera.ShakeCamera (1.5f, 2.5f);
		StaticJustinSpawner.SetActive (false);
	}

	void Start()
	{
		GameObject particleEffect = Instantiate (ErnaniAppear, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 1), Quaternion.identity);
		BackgroundMusic theMusic = GameObject.Find ("BackgroundMusic").GetComponent<BackgroundMusic> ();
		theMusic.Boss ();
		stephenTransform = GameObject.Find ("Stephen").GetComponent<Transform> ();
		shootCD = 3f;
	}

	void FixedUpdate()
	{
		if (dead == false)
			Shoot ();
		if (dead == true)
			End ();
		if (myHealth <= 500f) {
			StaticJustinSpawner.SetActive (true);
		}
	}

	void Update () {
		time -= Time.deltaTime;
		shootCD -= Time.deltaTime;
		stephenTransform = GameObject.Find ("Stephen").GetComponent<Transform> ();
		mySpriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
		if (stephenTransform.position.x > transform.position.x) 
			mySpriteRenderer.flipX = false;
		else if (stephenTransform.position.x < transform.position.x) 
			mySpriteRenderer.flipX = true;		
		Death ();
	}

	void Shoot()
	{

		if (shootCD < 0) {
			AudioSource.PlayClipAtPoint (laser, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), 0.33f);	
			GameObject beam = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
			Transform player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
			Vector3 direction = (player.position - beam.transform.position).normalized;
			beam.GetComponent<Rigidbody2D> ().velocity = direction * 25f;
			shootCD = shootCDTime;
		}

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.transform.tag == "Environment") {
			movingLeft = !movingLeft;
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		//anim = this.transform.GetComponent<Animator> ();
		if (col.transform.tag == "Projectile") 
		{
			missile = col.GetComponent<Projectile> ();
			if (myHealth != missile.damage)
				AudioSource.PlayClipAtPoint (hurt, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), 0.33f);	
			myHealth -= missile.damage;
			//anim.SetTrigger ("Hurt");
		}


	}
	void Death()
	{
		if (myHealth <= 0 && dead == false) {
			PlayerMovement2 stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement2> ();
			stephen.bossIsDead = true;
			dead = true;
			GameObject particleEffect = Instantiate (ErnaniAppear, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 1), Quaternion.identity);
			AudioSource.PlayClipAtPoint (death, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), 0.33f);
			PlayerMovement2.kills += 50;
			BackgroundMusic theMusic = GameObject.Find ("BackgroundMusic").GetComponent<BackgroundMusic> ();
			theMusic.Pause ();
		}
	}
	void End()
	{
		if (once == false) 
		{
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (1.5f, .9f);
			GameObject Explode = Instantiate (ExplosiveEffect, new Vector3 (4f, 6.2f, 0f), Quaternion.identity) as GameObject;
			time = timeTime;
			once = true;
			twice = true;
		}
		if (time <= 0 && twice == true) 
		{
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (1.5f, .9f);
			GameObject Explode2 = Instantiate (ExplosiveEffect, new Vector3 (15f, 4f, 0f), Quaternion.identity) as GameObject;
			time = timeTime;
			twice = false;
			thrice = true;
		}

		if (time <=0 && thrice == true)
		{
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (1.5f, .9f);
			GameObject Explode3 = Instantiate (ExplosiveEffect, new Vector3 (1.5f, 5.7f, 0f), Quaternion.identity) as GameObject;
			time = timeTime;
			thrice = false;
			quadruple = true;
			healthBar.SetActive (false);
		}
		if (time <= 0 && quadruple == true) 
		{
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (1.5f, .9f);
			GameObject Explode4 = Instantiate (ExplosiveEffect, new Vector3 (6.65f, 6.9f, 0f), Quaternion.identity) as GameObject;
			time = timeTime;
			quadruple = false;
			five = true;
		}
		if (time <= 0 && five == true) 
		{
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (1.5f, .9f);
			GameObject Explode5 = Instantiate (ExplosiveEffect, new Vector3 (16.5f, 5.74f, 0f), Quaternion.identity) as GameObject;
			time = timeTime;
			five = false;
			last = true;
		}
		if (time <= 0 && last == true) 
		{
			LevelManager levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
			levelManager.LoadNextLevel ();
		}
	}
}
