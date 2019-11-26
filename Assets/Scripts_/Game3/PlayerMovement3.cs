using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3 : MonoBehaviour {

	public GameObject nathanspawns;
	public GameObject SmokePuff;
	public static float movementSpeed = 7f;
	public static float rocketSpeed = 7f;
	public static float descendSpeed = -3f;
	public bool grounded2 = true;
	public float health = 100f;
	public AudioClip Laser;
	public AudioClip jetpack;
	public AudioClip hurt;
	public AudioClip death;
	public int totalKills;
	public GameObject manuel;

	private LevelManager levelManager;
	private Animator anim;
	private Rigidbody2D stephen;
	private SpriteRenderer mySpriteRenderer;

	public bool bossIsDead = false;
	bool reallyDead = false;
	public static int kills;
	public GameObject projectile;
	bool PlayerDescending;
	bool playerShooting = false;
	float shootTime;
	public static float shootCoolDown = 0.13f;
	float fuelRegenRate = 12f;

	void Start () {
		totalKills = PlayerMovement2.kills;
		kills = totalKills;
	}
		
	void Update()
	{
		Shoot ();
		Move ();
		if (reallyDead == true) {
			nathanspawns.SetActive (true);
			if (health <= 0f) {
				levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
				levelManager.LoadLevel ("Start");
			}
		}
		else if (health <= 0f) 
		{
			health = 50f;
			reallyDead = true;
			GameObject savior = Instantiate (manuel, new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
			StartCoroutine (quickPause ());
		}
		else if (Input.GetKeyDown(KeyCode.P))
			kills = totalKills;	
	}
	IEnumerator quickPause()
	{
		print ("waiting");
		Time.timeScale = 0f;
		float pausedEndTime = Time.realtimeSinceStartup + 3f;
		while (Time.realtimeSinceStartup < pausedEndTime) 
		{
			yield return 0;
		}
		Time.timeScale = 1f;
		print ("wait done");
	}
	void Move()
	{

		mySpriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
		Vector3 playerPos = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);
		if (Input.GetKey (KeyCode.A)) {
			playerPos.x -= movementSpeed * Time.deltaTime;
			mySpriteRenderer.flipX = true;
			if (Input.GetKey (KeyCode.Space))
			{
				GameObject.Find ("Stephen").GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f,rocketSpeed);
			}
		} 
		else if (Input.GetKey (KeyCode.D)) 
		{
			playerPos.x += movementSpeed * Time.deltaTime;
			mySpriteRenderer.flipX = false;
			if (Input.GetKey (KeyCode.Space))
			{
				GameObject.Find ("Stephen").GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f,rocketSpeed);
			}
		} 
		if (Input.GetKey (KeyCode.Space))
		{
			GameObject.Find ("Stephen").GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f,rocketSpeed);
		}

		if (Input.GetKey (KeyCode.S)) {
			GameObject.Find ("Stephen").GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, descendSpeed);
		}

		this.transform.position = playerPos;
		anim = this.transform.GetComponent<Animator> ();
		anim.SetBool ("grounded", grounded2);//Flying Animation
		anim.SetFloat ("speed", Mathf.Abs(Input.GetAxis ("Horizontal")));//Running Animation
		anim.SetBool ("playerDescending", PlayerDescending);//Descending Animation
		float fallSpeed = Input.GetAxis ("Vertical"); //Checks if Descending
		if (fallSpeed < 0)
			PlayerDescending = true;
		else if (fallSpeed >= 0)
			PlayerDescending = false;
	}


	void Shoot()
	{
		if (Input.GetMouseButton(0) && playerShooting == false) {
			GameObject beam = Instantiate (projectile, this.transform.position, Quaternion.identity) as GameObject;
			Vector3 sp = Camera.main.WorldToScreenPoint (transform.position);
			Vector3 dir = (Input.mousePosition - sp).normalized;
			beam.GetComponent<Rigidbody2D> ().velocity = dir * 25f;
			playerShooting = true;
			shootTime = shootCoolDown;
			AudioSource.PlayClipAtPoint (Laser, this.transform.position,0.45f);
		}
		if (playerShooting == true) 
		{
			if (shootTime > 0) {
				shootTime -= Time.deltaTime;
			} else if (shootTime < 0 || shootTime == 0)
				playerShooting = false;
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		grounded2 = true;
	}

	void OnTriggerExit2D (Collider2D col)
	{
		grounded2 = false;
		GameObject smoke = Instantiate (SmokePuff, new Vector3 (transform.position.x, transform.position.y - 0.73f, transform.position.z - 1f), Quaternion.identity) as GameObject;
	}
}
