using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrickBehavior : MonoBehaviour {

	public GameObject smoke;
	public GameObject Appear;
	public GameObject ExplosiveEffect;
	public GameObject healthBar;
	public AudioClip thunder;

	private SpriteRenderer mySpriteRenderer;
	private PlayerMovement4 stephen;
	private Rigidbody2D stephenBody;
	private PlayerMovement player;
	private Projectile4 missile;
	private Animator anim;
	public GameObject projectile;

	public float speed = 15f;
	public float shootCooldown = 2f;
	private float shootCD;
	public float myHealth = 10000f; //Patrick's health
	public float myDamage = 25f; //Patrick's damage
	private bool dead = false;
	bool movingLeft = true;
	bool attacking;

	bool once = false;
	bool twice = false;
	bool thrice = false;
	bool quadruple = false;
	bool five = false;
	bool last = false;
	bool appearing = true;

	float timeTime = 0.9f;
	float time;

	void Awake()
	{
		PlayerMovement4.christianDead = true;
		stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement4> ();
		stephen.bossIsDead = false;
		healthBar.SetActive (true);
		CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
		camera.ShakeCamera (1.5f, 2.5f);
	}

	void Start()
	{
		anim = GameObject.Find("Patrick").GetComponent<Animator> ();
		anim.SetBool ("appearing", appearing);
		shootCD = 3f; 
		mySpriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
		mySpriteRenderer.flipX = true;
	}
	void FixedUpdate()
	{
		if (dead == false) {
			Shoot2 ();
			//Shoot ();
		}
		if(dead == true)
			End ();
	}
	void Update () 
	{
		time -= Time.deltaTime;
		shootCD -= Time.deltaTime;
		mySpriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
		Transform stephenTransform = GameObject.Find("Stephen").GetComponent<Transform>();
		if (myHealth <= 4000) 
		{
			shootCooldown = 1.75f;
			if (myHealth <= 3000)
			{
				shootCooldown = 1.5f;
				if (myHealth <= 2000)
				{
					shootCooldown = 1.25f;
					if (myHealth <= 1000) 
					{
						shootCooldown = 1f;
					}
				}
			}
		}
		if (stephenTransform.position.x > transform.position.x) {
			mySpriteRenderer.flipX = false;
			movingLeft = false;
		} 
		if (stephenTransform.position.x < transform.position.x) {
			mySpriteRenderer.flipX = true;
			movingLeft = true;
		}
		this.transform.position = Vector2.MoveTowards (this.transform.position, stephenTransform.position, speed * Time.deltaTime);

		Death ();
	}
	void Shoot()
	{
		anim = GameObject.Find("Patrick").GetComponent<Animator> ();
		attacking = false;
		anim.SetBool ("attacking", attacking);
		if (shootCD <= 0f) {
			attacking = true;
			anim.SetBool ("attacking", attacking);
			AudioSource.PlayClipAtPoint (thunder, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), 1f);	
			Transform stephenTransform = GameObject.Find("Stephen").GetComponent<Transform>();
			int xpos = Random.Range (-9, 10);
			float xpos2 = (float)xpos;
			GameObject beam = Instantiate (projectile, new Vector3 (xpos2,10f, stephenTransform.position.z), Quaternion.identity) as GameObject;
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			Transform thePlayer = player.transform;
			Vector3 direction = (thePlayer.position - beam.transform.position).normalized;
			beam.GetComponent<Rigidbody2D> ().velocity = direction * 3; 
			shootCD = shootCooldown; 
		}

	}
	void Shoot2()
	{
		anim = GameObject.Find("Patrick").GetComponent<Animator> ();
		attacking = false;
		anim.SetBool ("attacking", attacking);
		if (shootCD <= 0f) {
			attacking = true;
			anim.SetBool ("attacking", attacking);
			AudioSource.PlayClipAtPoint (thunder, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), 1f);	
			Transform stephenTransform = GameObject.Find("Stephen").GetComponent<Transform>();
			GameObject beam = Instantiate (projectile, new Vector3(stephenTransform.position.x, stephenTransform.position.x + 15f, stephenTransform.position.z), Quaternion.identity) as GameObject;
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			Transform thePlayer = player.transform;
			Vector3 direction = (thePlayer.position - beam.transform.position).normalized;
			beam.GetComponent<Rigidbody2D> ().velocity = direction * 4; 
			shootCD = shootCooldown; 
		}
	}
	void OnCollisionEnter2D (Collision2D col)
	{
		

		if (col.transform.tag == "Projectile") 
		{
			Transform stephenTransform = GameObject.Find("Stephen").GetComponent<Transform>();
			GameObject theSmoke = Instantiate (smoke, this.transform.position, Quaternion.identity);
			stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement4> ();
			missile = col.gameObject.GetComponent<Projectile4> ();
			//if (myHealth != missile.damage)
			//AudioSource.PlayClipAtPoint (hurt, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), .33f);	
			myHealth -= missile.damage;
			appearing = false;
			stephen.energy += 5f;
			int xpos = Random.Range (-9, 10);
			int ypos = Random.Range(-3,-1);
			float xpos2 = (float)xpos;
			float ypos2 = (float)ypos;
			transform.position = new Vector3 (xpos2, ypos2, this.transform.position.z);
			this.transform.position = Vector2.MoveTowards (this.transform.position, stephenTransform.position, speed * Time.deltaTime);
		}
		if (col.transform.tag == "Player") 
		{
			stephenBody = GameObject.Find ("Stephen").GetComponent<Rigidbody2D> ();
			stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement4> ();
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (0.25f, 1f);
			stephen.energy += 5f;
			stephen.health -= myDamage;
			AudioSource.PlayClipAtPoint (stephen.hurt, col.transform.position, 1f);
			if (movingLeft == true) {
				stephenBody.velocity = new Vector3 (-10f, 5f, 0f);
			} else if (movingLeft == false) {
				stephenBody.velocity = new Vector3 (10f, 5f, 0f);
			}
		}
	}
	void Death()
	{
		if (myHealth <= 0 && dead == false) {
			healthBar.SetActive (false);
			dead = true;
			stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement4> ();
			stephen.bossIsDead = true;
			//AudioSource.PlayClipAtPoint (death, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), .33f);
			PlayerMovement4.kills += 9999;
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
			LevelManager levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
			levelManager.LoadNextLevel ();
			Destroy (gameObject);
		}
	}
}
