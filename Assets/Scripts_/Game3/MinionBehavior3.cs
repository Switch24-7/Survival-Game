using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBehavior3 : MonoBehaviour {

	public GameObject bloodParticle;
	public GameObject explosiveParticle;
	public AudioClip hurt;
	public AudioClip death;
	public AudioClip laser;
	public GameObject projectile;

	private SpriteRenderer mySpriteRenderer;
	private PlayerMovement3 stephen;
	private Rigidbody2D stephenBody;
	private Transform stephenTransform;
	private Projectile3 missile;
	private Animator anim;

	public float shootCooldown = 2f;
	private float shootCD;
	public float myHealth = 30f; //nathan's health
	public float myDamage = 5f; //nathan's damage 
	public float speed = 3f;
	public bool dead = false;

	void Update () {
		shootCD -= Time.deltaTime;
		Shoot ();
		Death ();
		mySpriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
		stephenTransform = GameObject.Find("Stephen").GetComponent<Transform>();
		this.transform.position = Vector2.MoveTowards (this.transform.position, stephenTransform.position, speed * Time.deltaTime);
		//transform.right = stephenTransform.position - transform.position;
		if (stephenTransform.position.x > transform.position.x)
			mySpriteRenderer.flipX = false;
		else if (stephenTransform.position.x < transform.position.x)
			mySpriteRenderer.flipX = true;

	}
	void Shoot()
	{

		if (shootCD <= 0f) {
			AudioSource.PlayClipAtPoint (laser, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), .33f);	
			GameObject beam = Instantiate (projectile, this.transform.position, Quaternion.identity) as GameObject;
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			Transform thePlayer = player.transform;
			Vector3 direction = (thePlayer.position - beam.transform.position).normalized;
			beam.GetComponent<Rigidbody2D> ().velocity = direction * 5; 
			shootCD = shootCooldown; 
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{

		stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement3> ();
		stephenBody = GameObject.Find ("Stephen").GetComponent<Rigidbody2D> ();
		anim = this.transform.GetComponent<Animator> ();
		if (col.transform.tag == "Projectile") 
		{
			GameObject blood = Instantiate (bloodParticle, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity) as GameObject;
			missile = col.gameObject.GetComponent<Projectile3> ();
			if (myHealth != missile.damage)
				AudioSource.PlayClipAtPoint (hurt, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), 1f);	
			myHealth -= missile.damage;
			anim.SetTrigger ("Hurt");
		}
		else if (col.gameObject.tag == "Player") {
			stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement3> ();
			if (stephen.bossIsDead == false) {
				GameObject explosion = Instantiate (explosiveParticle, this.transform.position, Quaternion.identity) as GameObject;
				CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
				camera.ShakeCamera (0.25f, 1f);
				stephen.health -= myDamage;
				AudioSource.PlayClipAtPoint (stephen.hurt, col.transform.position);
			}
			Destroy (this.gameObject);
		}

	}
	void Death()
	{
		if (myHealth <= 0) {
			dead = true;
			AudioSource.PlayClipAtPoint (death, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), 1f);
			PlayerMovement3.kills++;
			Destroy (this.gameObject);
		}
	}
}
