using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public static float movementSpeed = 7f;
	public static float rocketSpeed = 7f;
	public bool grounded2 = true;
	public float health = 100f;
	public GameObject SmokePuff;
	public AudioClip Laser;
	public AudioClip jetpack;
	public AudioClip hurt;
	public AudioClip death;

	private LevelManager levelManager;
	private Animator anim;
	private Rigidbody2D stephen;
	private SpriteRenderer mySpriteRenderer;
	private DoorExit door;

	public int totalKills;
	public static int kills;
	public GameObject projectile;
	bool PlayerDescending;
	bool playerShooting = false;
	public bool bossIsDead = false;
	float shootTime;
	public static float shootCoolDown = 0.2f;
	float fuelRegenRate = 12f;

	void Start () {
		kills = totalKills;
	}
		
	void FixedUpdate()
	{
		Shoot ();
	}
	void Update()
	{
		if (bossIsDead == false) {
			if (health <= 0f || this.gameObject == null) {
				levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
				levelManager.LoadLevel ("Start");
			} 
		}
		else if (Input.GetKeyDown (KeyCode.P)) 
		{
			kills = totalKills;
		}
		Move ();
	}

	void Move()
	{
		
		mySpriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
		Vector3 playerPos = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);
		if (Input.GetKey (KeyCode.A)) {
			playerPos.x -= movementSpeed * Time.deltaTime;
			mySpriteRenderer.flipX = true;
			if (Input.GetKeyDown (KeyCode.Space) && grounded2 == true)
			{
				GameObject.Find ("Stephen").GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f,rocketSpeed);
			}
		} 
		else if (Input.GetKey (KeyCode.D)) 
		{
			playerPos.x += movementSpeed * Time.deltaTime;
			mySpriteRenderer.flipX = false;
			if (Input.GetKeyDown (KeyCode.Space) && grounded2 == true)
			{
				GameObject.Find ("Stephen").GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f,rocketSpeed);
			}
		} 
		if (Input.GetKeyDown (KeyCode.Space) && grounded2 == true)
		{
            GameObject.Find("Stephen").GetComponent<Rigidbody2D>().velocity = new Vector2(0f, rocketSpeed);
        }
		
		this.transform.position = playerPos;
		anim = this.transform.GetComponent<Animator> ();
		anim.SetBool ("grounded", grounded2);//Flying Animation
		anim.SetFloat ("speed", Mathf.Abs(Input.GetAxis ("Horizontal")));//Running Animation
	}
		

	void Shoot()
	{
		if (Input.GetMouseButton(0) && playerShooting == false) {
			GameObject beam = Instantiate (projectile, this.transform.position, Quaternion.identity) as GameObject;
			Vector3 sp = Camera.main.WorldToScreenPoint (transform.position);
			Vector3 dir = (Input.mousePosition - sp).normalized;
			beam.GetComponent<Rigidbody2D> ().velocity = dir * 15f;
			playerShooting = true;
			shootTime = shootCoolDown;
			AudioSource.PlayClipAtPoint (Laser, this.transform.position, 0.45f);
		}
		/*else if (Input.GetMouseButton(0) && mySpriteRenderer.flipX == true && playerShooting == false) {
			GameObject beam = Instantiate (projectile, this.transform.position, Quaternion.identity) as GameObject;
			beam.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-20f, 0f);
			playerShooting = true;
			shootTime = shootCoolDown;
			AudioSource.PlayClipAtPoint (Laser, this.transform.position);
		}
		*/
		if (playerShooting == true) 
		{
			if (shootTime > 0) {
				shootTime -= Time.deltaTime;
			} else if (shootTime < 0 || shootTime == 0)
				playerShooting = false;
		}
		//anim.SetBool ("Shooting", playerShooting);
		

	}

	void OnTriggerEnter2D (Collider2D col)
	{
		grounded2 = true;

	}

	void OnTriggerExit2D (Collider2D col)
	{
		GameObject smoke = Instantiate (SmokePuff, new Vector3 (transform.position.x, transform.position.y - 0.73f, transform.position.z - 1f), Quaternion.identity) as GameObject;
		grounded2 = false;

	}
}
