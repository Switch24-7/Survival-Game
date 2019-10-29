using UnityEngine;
using System.Collections;

public class JonasBehavior : MonoBehaviour {

	public float myHealth = 20f;
	public float myDamage = 5f;
	public float speed = 3f;

	private bool movingLeft = true;
	private Rigidbody2D stephenBody;
	private PlayerMovement4 stephen;
	private Projectile4 missile;
	private SpriteRenderer jonas;
	private Animator anim;

	void Start () {
	
	}
	
	void FixedUpdate()
	{
		if (myHealth <= 0) 
		{
			Destroy (this.gameObject);
		}
	}

	void Update () {
		SpriteRenderer mySpriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
		Transform stephenTransform = GameObject.Find("Stephen").GetComponent<Transform>();
		this.transform.position = Vector2.MoveTowards (this.transform.position, stephenTransform.position, speed * Time.deltaTime);
		//transform.right = stephenTransform.position - transform.position;
		if (stephenTransform.position.x > transform.position.x)
			mySpriteRenderer.flipX = false;
		else if (stephenTransform.position.x < transform.position.x)
			mySpriteRenderer.flipX = true;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		stephen = GameObject.Find ("Stephen").GetComponent<PlayerMovement4> ();
		stephenBody = GameObject.Find ("Stephen").GetComponent<Rigidbody2D> ();
		if (col.transform.tag == "Player") {
			stephen.health -= myDamage;
			Destroy (gameObject);
		} 
		if (col.transform.tag == "Projectile") {
			anim = this.gameObject.GetComponent<Animator> ();
			anim.SetTrigger ("Hurt");
			missile = col.gameObject.GetComponent<Projectile4> ();
			myHealth -= missile.damage;
		}
	}
}
