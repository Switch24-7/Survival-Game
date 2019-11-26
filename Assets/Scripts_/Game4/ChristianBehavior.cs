using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristianBehavior : MonoBehaviour {

	public GameObject healthBar;
	public GameObject RichardAppear;
	public GameObject ExplosiveEffect;
	public AudioClip hurt;
	public AudioClip death;
	public AudioClip laser;

	private SpriteRenderer mySpriteRenderer;
	private Transform stephenTransform;
	private Projectile4 missile;

	public GameObject projectile;
	public float myHealth = 3000f; //Christian's health
	public bool dead = false;
	public float speed;
	public float shootCDTime = .7f;
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
	}

	void Start()
	{
		GameObject particleEffect = Instantiate (RichardAppear, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 1), Quaternion.identity);
		stephenTransform = GameObject.Find ("Stephen").GetComponent<Transform> ();
		shootCD = 3f;
	}

	void FixedUpdate()
	{
		if (dead == false)
			Shoot ();

	}

	void Update () {
		if (myHealth <= 2500f) {
			shootCDTime = .6f;
			if (myHealth <= 2000f) 
			{
				shootCDTime = .5f;
				if (myHealth <= 1500f) 
				{
					shootCDTime = .2f;
					if (myHealth <= 1000f) {
						shootCDTime = .1f;
						if (myHealth <= 500f)
							shootCDTime = 0.05f;
					}
				}
			}
		}
		time -= Time.deltaTime;
		shootCD -= Time.deltaTime;
		stephenTransform = GameObject.Find ("Stephen").GetComponent<Transform> ();
		mySpriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
		if (stephenTransform.position.x > transform.position.x) 
			mySpriteRenderer.flipX = true;
		else if (stephenTransform.position.x < transform.position.x) 
			mySpriteRenderer.flipX = false;	
		if(dead == true)
			End ();
		Death ();
	}

	void Shoot()
	{

		if (shootCD < 0) {
			AudioSource.PlayClipAtPoint (laser, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), 0.33f);	
			GameObject beam = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
			Transform player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
			Vector3 direction = (player.position - beam.transform.position).normalized;
			beam.GetComponent<Rigidbody2D> ().velocity = direction * 6f;
			shootCD = shootCDTime;
		}

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.transform.tag == "Environment") {
			movingLeft = !movingLeft;
		}
		if (col.transform.tag == "Projectile") 
		{
			missile = col.gameObject.GetComponent<Projectile4> ();
			myHealth -= missile.damage;
		}
	}

	void Death()
	{
		if (myHealth <= 0 && dead == false) {
			healthBar.SetActive (false);
			dead = true;
			PlayerMovement4 stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement4> ();
			stephen.bossIsDead = true;
			PlayerMovement4.christianDead = true;
			PlayerMovement4.kills += 100;
		}
	}
	void End()
	{
		if (once == false) 
		{
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (1.5f, .9f);
			GameObject Explode = Instantiate (ExplosiveEffect, new Vector3 (-4f, 6.2f, 0f), Quaternion.identity) as GameObject;
			time = timeTime;
			once = true;
			twice = true;
		}
		if (time <= 0 && twice == true) 
		{
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (1.5f, .9f);
			GameObject Explode2 = Instantiate (ExplosiveEffect, new Vector3 (-5f, 4f, 0f), Quaternion.identity) as GameObject;
			time = timeTime;
			twice = false;
			thrice = true;
		}

		if (time <=0 && thrice == true)
		{
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (1.5f, .9f);
			GameObject Explode3 = Instantiate (ExplosiveEffect, new Vector3 (1.5f, 1.7f, 0f), Quaternion.identity) as GameObject;
			time = timeTime;
			thrice = false;
			quadruple = true;
			healthBar.SetActive (false);
		}
		if (time <= 0 && quadruple == true) 
		{
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (1.5f, .9f);
			GameObject Explode4 = Instantiate (ExplosiveEffect, new Vector3 (0f, 3f, 0f), Quaternion.identity) as GameObject;
			time = timeTime;
			quadruple = false;
			five = true;
		}
		if (time <= 0 && five == true) 
		{
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (1.5f, .9f);
			GameObject Explode5 = Instantiate (ExplosiveEffect, new Vector3 (1f, 4f, 0f), Quaternion.identity) as GameObject;
			time = timeTime;
			five = false;
			last = true;
		}
		if (time <= 0 && last == true) 
		{
			LevelManager levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
			levelManager.LoadNextLevel ();
			Destroy (gameObject);
		}
	}
}
