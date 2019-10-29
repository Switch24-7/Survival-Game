using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristopherBehavior : MonoBehaviour {

	public GameObject Appear;
	public GameObject Sai;
	public GameObject ExplosiveEffect;
	public GameObject healthBar;
	public AudioClip hurt;
	public AudioClip death;
	public AudioClip laser;

	private SpriteRenderer mySpriteRenderer;
	private PlayerMovement4 stephen;
	private Rigidbody2D stephenBody;
	private PlayerMovement player;
	private Projectile4 missile;
	private Animator anim;
	public GameObject projectile;

	public float shootCooldown = 1.1f;
	private float shootCD;
	public float myHealth = 3000f; //Christopher's health
	public float myDamage = 5f; //Christopher's damage 
	private bool dead = false;
	bool movingLeft = true;

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
		GameObject particleEffect = Instantiate (Appear, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 1), Quaternion.identity);
		shootCD = 3f; 
		mySpriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
		mySpriteRenderer.flipX = true;
	}
	void FixedUpdate()
	{
		if (dead == false)
		Shoot ();
		if(dead == true)
			End ();
	}
	void Update () 
	{
		time -= Time.deltaTime;
		shootCD -= Time.deltaTime;
		if (myHealth <= 2500f) {
			shootCooldown = 0.75f;
			if (myHealth <= 2000f) 
			{
				shootCooldown = 0.5f;
				if (myHealth <= 1500f) 
				{
					shootCooldown = 0.25f;
					if (myHealth <= 1000f) {
						shootCooldown = 0.2f;
						if (myHealth <= 500f)
							shootCooldown = 0.1f;
					}
				}
			}
		}
		mySpriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
		Transform stephenTransform = GameObject.Find("Stephen").GetComponent<Transform>();
		if (stephenTransform.position.x > transform.position.x) {
			mySpriteRenderer.flipX = false;
			movingLeft = false;
		} else if (stephenTransform.position.x < transform.position.x) {
			mySpriteRenderer.flipX = true;
			movingLeft = true;
		}
		Death ();
	}
	void Shoot()
	{
		if (shootCD <= 0f) {
			AudioSource.PlayClipAtPoint (laser, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), .33f);	
			GameObject beam = Instantiate (projectile, this.transform.position, Quaternion.identity) as GameObject;
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			Transform thePlayer = player.transform;
			Vector3 direction = (thePlayer.position - beam.transform.position).normalized;
			beam.GetComponent<Rigidbody2D> ().velocity = direction * 20; 
			shootCD = shootCooldown; 
		}
	}
	void OnCollisionEnter2D (Collision2D col)
	{
		stephenBody = GameObject.Find ("Stephen").GetComponent<Rigidbody2D> ();

		if (col.transform.tag == "Projectile") 
		{
			missile = col.gameObject.GetComponent<Projectile4> ();
			//if (myHealth != missile.damage)
			//AudioSource.PlayClipAtPoint (hurt, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), .33f);	
			myHealth -= missile.damage;
		}
			
	}
	void Death()
	{
		if (myHealth <= 0 && dead == false) {
			healthBar.SetActive (false);
			dead = true;
			//AudioSource.PlayClipAtPoint (death, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), .33f);
			PlayerMovement4.kills += 100;
		}
	}
	void End()
	{
		if (once == false) 
		{
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (1.5f, .9f);
			GameObject Explode = Instantiate (ExplosiveEffect, new Vector3 (-2f, 3.2f, 0f), Quaternion.identity) as GameObject;
			time = timeTime;
			once = true;
			twice = true;
		}
		if (time <= 0 && twice == true) 
		{
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (1.5f, .9f);
			GameObject Explode2 = Instantiate (ExplosiveEffect, new Vector3 (0f, 1f, 0f), Quaternion.identity) as GameObject;
			time = timeTime;
			twice = false;
			thrice = true;
		}

		if (time <=0 && thrice == true)
		{
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (1.5f, .9f);
			GameObject Explode3 = Instantiate (ExplosiveEffect, new Vector3 (-4f, 3.7f, 0f), Quaternion.identity) as GameObject;
			time = timeTime;
			thrice = false;
			quadruple = true;
			healthBar.SetActive (false);
		}
		if (time <= 0 && quadruple == true) 
		{
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (1.5f, .9f);
			GameObject Explode4 = Instantiate (ExplosiveEffect, new Vector3 (-5f, 4.9f, 0f), Quaternion.identity) as GameObject;
			time = timeTime;
			quadruple = false;
			five = true;
		}
		if (time <= 0 && five == true) 
		{
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (1.5f, .9f);
			GameObject Explode5 = Instantiate (ExplosiveEffect, new Vector3 (-1f, 2.74f, 0f), Quaternion.identity) as GameObject;
			time = timeTime;
			five = false;
			last = true;
		}
		if (time <= 0 && last == true) 
		{
			GameObject particleEffect = Instantiate (Appear, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 1), Quaternion.identity);
			Sai.SetActive (true);
			Destroy (gameObject);
		}
	}
}
