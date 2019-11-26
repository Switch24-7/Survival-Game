using UnityEngine;
using System.Collections;

public class JustinBehavior : MonoBehaviour {

	public GameObject bloodParticle;
	public AudioClip hurt;
	public AudioClip death;
	public AudioClip laser;

	private SpriteRenderer mySpriteRenderer;
	private PlayerMovement stephen;
	private Rigidbody2D stephenBody;
	private PlayerMovement player;
	private Projectile missile;
	private Animator anim;
	public GameObject projectile;

	public float shootCooldown = 2f;
	private float shootCD;
	public float myHealth = 500f; //justin's health
	public float myDamage = 50f; //justin's damage 
	private bool dead = false;
	bool movingLeft = true;

	void Start()
	{
		shootCD = shootCooldown; 
		mySpriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
		mySpriteRenderer.flipX = true;
	}

	void FixedUpdate()
	{
		Shoot ();
		shootCD -= Time.deltaTime;
	}

	void Update () 
	{
		Death ();
		mySpriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
		Transform stephenTransform = GameObject.Find("Stephen").GetComponent<Transform>();
		if (stephenTransform.position.x > transform.position.x) {
			mySpriteRenderer.flipX = false;
			movingLeft = false;
		} else if (stephenTransform.position.x < transform.position.x) {
			mySpriteRenderer.flipX = true;
			movingLeft = true;
		}
	}
		
	void Shoot()
	{
		
		if (shootCD <= 0f) {
			AudioSource.PlayClipAtPoint (laser, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), 0.33f);	
			GameObject beam = Instantiate (projectile, this.transform.position, Quaternion.identity) as GameObject;
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			Transform thePlayer = player.transform;
			Vector3 direction = (thePlayer.position - beam.transform.position).normalized;
			beam.GetComponent<Rigidbody2D> ().velocity = direction * 15; 
			shootCD = shootCooldown; 
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		mySpriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
		stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement> ();

		stephenBody = GameObject.Find ("Stephen").GetComponent<Rigidbody2D> ();

		if (col.gameObject.tag == "Player") {
			stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement> ();
			if (stephen.bossIsDead == false) {
				stephen.health -= myDamage;
				AudioSource.PlayClipAtPoint (stephen.hurt, col.transform.position, 1f);
				if (movingLeft == true) {
					stephenBody.velocity = new Vector3 (-10f, 5f, 0f);
				} else if (movingLeft == false) {
					stephenBody.velocity = new Vector3 (10f, 5f, 0f);
				}
			}
		}

		else if (col.transform.tag != "Floor") {
			movingLeft = !movingLeft;
			mySpriteRenderer.flipX = !mySpriteRenderer.flipX;
		} 
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		anim = this.transform.GetComponent<Animator> ();
		if (col.transform.tag == "Projectile") 
		{
			GameObject blood = Instantiate (bloodParticle, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity) as GameObject;
			missile = col.GetComponent<Projectile> ();
			if (myHealth != missile.damage)
				AudioSource.PlayClipAtPoint (hurt, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), .33f);	
			myHealth -= missile.damage;
			//anim.SetTrigger ("Hurt");
		}


	}
	void Death()
	{
		if (myHealth <= 0) {
			dead = true;
			Destroy (this.gameObject);
			AudioSource.PlayClipAtPoint (death, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), .33f);
			//player = GameObject.Find ("Stephen").GetComponent<PlayerMovement> ();
			//player.kills++;
			PlayerMovement.kills++;
		}
	}
}
