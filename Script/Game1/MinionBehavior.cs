using UnityEngine;
using System.Collections;

public class MinionBehavior : MonoBehaviour {

	public GameObject dropLoot;
	public GameObject bloodParticle;
	public GameObject explosiveParticle;
	public AudioClip hurt;
	public AudioClip death;

	private EnemySpawner enemySpawner;
	private SpriteRenderer mySpriteRenderer;
	private PlayerMovement stephen;
	private Rigidbody2D stephenBody;
	private Transform stephenTransform;
	//private PlayerMovement player;
	private Projectile missile;
	private Animator anim;

	public float myHealth = 150f; //nathan's health
	public float myDamage = 25f; //nathan's damage 
	public float speed = 3f;
	public bool dead = false;

	void Update () {
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

	void OnCollisionEnter2D (Collision2D col)
	{

		stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement> ();

		stephenBody = GameObject.Find ("Stephen").GetComponent<Rigidbody2D> ();

		if (col.gameObject.tag == "Player") {
			stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement> ();
			if (stephen.bossIsDead == false) 
				{
				GameObject explosion = Instantiate (explosiveParticle, this.transform.position, Quaternion.identity) as GameObject;
				CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
				camera.ShakeCamera (0.25f, 1f);
				stephen.health -= myDamage;
				AudioSource.PlayClipAtPoint (stephen.hurt, col.transform.position);
			}
			Destroy (this.gameObject);
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
				AudioSource.PlayClipAtPoint (hurt, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), 1f);	
			myHealth -= missile.damage;
			anim.SetTrigger ("Hurt");
		}

			
	}
	void Death()
	{
		if (myHealth <= 0) {
			dead = true;
			//AudioSource.PlayClipAtPoint (death, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 5f), 1f);
			PlayerMovement.kills++;
			DropChance ();
			Destroy (this.gameObject);
		}
	}
	void DropChance()
	{
		int randomNumber = Random.Range (0, 101);
		if (randomNumber <= 10) {
			GameObject drop = Instantiate (dropLoot, this.transform.position, Quaternion.identity) as GameObject;
		}

	}
}
