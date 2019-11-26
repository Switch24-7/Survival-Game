using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement4 : MonoBehaviour {

	public GameObject manuel;
	public GameObject saiyan;
	public GameObject SmokePuff;
	public static float movementSpeed = 3.5f;
	public static float rocketSpeed = 3.5f;
	public static float descendSpeed = -1.5f;
	public bool grounded2 = true;
	public float health = 100f;
	public AudioClip Laser;
	public AudioClip jetpack;
	public AudioClip hurt;
	public AudioClip death;
	public int totalKills;
	public float energy = 0f;

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
	RichardBehavior richard;
	bool awakened = false;
	public float awakeTime = 10f;
	float awake;
	public static bool christianDead;

	void Start () {
		if (christianDead == false) {
			totalKills = PlayerMovement3.kills;
			kills = totalKills;
		}
	}

	void Update()
	{
		energy += .001f;
		awake -= Time.deltaTime;
		Shoot ();
		Move ();
		if (energy >= 100f) {
			energy = 100f;
		}


		if (health <= 0f && bossIsDead == false) {
			if (christianDead == true) {
				levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
				levelManager.LoadLevel ("WinLevel4");
			}
			else
			{
				levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
				levelManager.LoadLevel ("Start");
			}
		}
		if (awakened == true) {
			energy = 0f;
			CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			camera.ShakeCamera (0.15f, awake);
			manuel.SetActive (true);
			saiyan.SetActive (true);
			movementSpeed = 7f;
			rocketSpeed = 7f;
			descendSpeed = -3f;
			shootCoolDown = 0.05f;
			health += 0.25f;
		} 
		if (awake <= 0)
			awakened = false;
		if (awakened == false) {
			manuel.SetActive (false);
			saiyan.SetActive (false);
			movementSpeed = 3.5f;
			rocketSpeed = 3.5f;
			descendSpeed = -1.5f;
			shootCoolDown = 0.13f;
		}
		if (Input.GetKeyDown (KeyCode.X) && energy == 100f) {
			awake = awakeTime;
			awakened = true;
		}
		if (Input.GetKeyDown(KeyCode.P))
			kills = totalKills;	
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
			beam.GetComponent<Rigidbody2D> ().velocity = dir * 12.5f;
			playerShooting = true;
			shootTime = shootCoolDown;
			AudioSource.PlayClipAtPoint (Laser, this.transform.position,0.45f);
			energy += 0.25f;
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
		GameObject smoke = Instantiate (SmokePuff, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1f), Quaternion.identity) as GameObject;
	}
}
