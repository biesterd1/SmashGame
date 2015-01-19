using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour 
{

	public enum Player {Player0 = 0, Player1 = 1, Player2 = 2, Player3 = 3, Player4 = 4};

	public float moveToAttackerCoeff = 10;
	public Player player;

	//int i = 0;

	public bool facingRight;							// For determining which way the player is currently facing.
	float direction = 1;
	public float acceleration = 10f;
	public float maxSpeed = 15f;				// The fastest the player can travel in the x axis.
	public float maxAirSpeed = 6f;			// Fastest the player can travel while airborne
	public float jumpForce = 800f;			// Amount of force added when the player jumps.	
	public float attackFriction = 1f;		// How much speed is reduced every frame (?) when attacking. Change in animator per attack if needed.
	//public float airFriction = .07f;			// Determines max speed of air movement horizontally
	public float grabForce = 800f;			// How much force is added hor to player when grabbing. 
	public float grabForceAirMultiple = 1.5f;	// Adds force while in air due to higher friction and more wanted mobility
	public float grabTime = .3f;
//	
//	[Range(0, 1)]										// Adds slider in Unity inspector (I think)
//	public float crouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
//	
	public LayerMask whatIsGround;			// A mask determining what is ground to the character
	public LayerMask whatIsBoundary;			// Determines what is spawn boundary
	


	bool grounded = false;								// Whether or not the player is grounded.

	Animator anim;										// Reference to the player's animator component.
	int jumpCount;
	public float flySpeed;
	public float flyDecay;
	
	public float fastSpeed = 10f;				// What determines "fast" movement speed, above this will trigger a running attack as opposed to regular attack.

	public static float grabTimer = 0;

	public static float xSpeed;							// Hor speed. Static used for debug purposes
	public static float ySpeed;

	public static bool fast;
	float storeMaxSpeed;								// Stores maxSpeed so it can be decreased while attacking/in air
	
	bool spawning = false;								// Is player spawning
	
	
	public bool attacking, airborne;
	public bool triggerAttackFriction = false;	// Used in animator to trigger horizontal friction when attacking
	public static bool staggered;

	private bool jump, attack, grab, up, down, crouch;
	
	public float v, h, hRaw, run;

	public float groundFriction;
	public float airFriction;
	public float speedBuffer = 1f;
	public static float moveToAttacker;

	// Controls
	string jumpKey, attackKey, grabKey, verticalKey, horizontalKey; 
	Transform groundCheck;

	Vector2 playerPosition;



	//public static GameObject player;
//
//	string tag;
//
//	public static GameObject self;
//	public static string selfName;
//	public static Transform target;
//
//
//	public List<GameObject> players = new List<GameObject>();
//	public List<string> playerNames = new List<string>();



	void Awake()
	{
		// Setting up references.
		anim = GetComponent<Animator>();
		anim.SetBool ("Instant", true);
		groundCheck = this.transform.Find("GroundCheck");
		storeMaxSpeed = maxSpeed;

		// Determines attributes for player
		switch (player) {
		case Player.Player0:
			Debug.Log ("This shouldn't happen");
			break;

		case Player.Player1:
			jumpKey = "Jump";
			attackKey = "Attack";
			grabKey = "Grab";
			horizontalKey = "Horizontal";
			verticalKey = "Vertical";
			break;

		case Player.Player2:
			jumpKey = "Jump2";
			attackKey = "Attack2";
			grabKey = "Grab2";
			horizontalKey = "Horizontal2";
			verticalKey = "Vertical2";
			break;
		}

		if (!facingRight) {
			Flip ();
			facingRight = !facingRight;
		}

	}

	void Update() {
		// Use this clamp function, but only have it true when running or moving. 
		// Keeps value in a range
		// Do not want it to be active if the player is attacked
		// rigidbody2D.velocity.x = Mathf.Clamp(rigidbody2D.velocity.x, -maxSpeed, maxSpeed);


		// Read the jump input in Update so button presses aren't missed.
		//Button vert = 
		v = Input.GetAxis (verticalKey);
		h = Input.GetAxisRaw (horizontalKey);
		hRaw = Input.GetAxisRaw (horizontalKey);
		jump = Input.GetButtonDown (jumpKey);
		attack = Input.GetButton (attackKey);
		grab = Input.GetButtonDown (grabKey);

		attacking = anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
		

		if (v > 0) {
			up = true;
			down = false;
		}
		if (v == 0 ) {
			up = false;
			down = false;
		}
		if (v < 0) {
			down = true;
			up = false;
		}
		//////////////////////////////////////////////////////////////////////////////////////

		//////////////////////////////////////////////////////////////////////////////////////
		// Determine if player is moving fast enough to trigger running attacks
		if (xSpeed >= fastSpeed) {
			fast = true;
			Debug.Log("Fast");
		}
		else {
			fast = false;
		}
		anim.SetBool ("Fast", fast);

		anim.SetBool ("Up", up);
		anim.SetBool ("Attack", attack);
		anim.SetBool ("Grab", grab);
		anim.SetBool ("Down", down);

		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

		anim.SetBool ("Spawning", spawning);

		// If the input is moving the player right and the player is facing left...
		if (h > 0 && !facingRight && !attacking) {
			// ... flip the player.
			Flip ();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (h < 0 && facingRight && !attacking) {
			// ... flip the player.
			Flip ();
		}
		
		// Reset jump counter to zero when hit ground
		// Resets staggered
		if (grounded) {
			jumpCount = 0;
			staggered = false;
		}
		
		// Jumping
		if ((jump && jumpCount<1) && !attacking) {
			// Add a vertical force to the player.
			anim.SetBool("Ground", false);
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			jumpCount++;
		}
		
		if (grab) {
			Grab();
			rigidbody2D.velocity = new Vector2(0,0);
		}

		ySpeed = Mathf.Abs(rigidbody2D.velocity.y);
		xSpeed = Mathf.Abs(rigidbody2D.velocity.x);

		if (ySpeed != 0) {
			airborne = true;		
		}
		else { 
			airborne = false;
		}

		grounded = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);


		anim.SetBool("Ground", grounded);
		anim.SetFloat ("Speed", xSpeed);
		anim.SetFloat("hSpeed", Mathf.Abs(h));


		// Player movement (h is horizontal axis of control)



		// Introduces drag when the player is in the air
//		if (grounded) {
//			rigidbody2D.drag = 	0;
//		}
//		else {
//			rigidbody2D.drag = airFriction;
//		}
	}

	void FixedUpdate() {
		playerPosition = new Vector2(rigidbody2D.position.x, rigidbody2D.position.y);
		run = Mathf.Abs(h * acceleration * Time.fixedDeltaTime);
		if (hRaw!=0 && !attacking && xSpeed <= maxSpeed/**Mathf.Abs(h)*/) {
			rigidbody2D.AddForce(new Vector2 (run*direction, 0f));
		}

		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed) {
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}
	}
	public void OnHit(float xForce, float yForce, float damage, float attackDirection, Vector2 attackerPosition, bool first, float multiplier) {
		moveToAttacker = attackerPosition.x - playerPosition.x;
		Vector2 fly;
		// sets velocity of player being hit to zero to remove momentum (may change)

		rigidbody2D.velocity = new Vector2(0,0);

		if (first == true) {
			fly = new Vector2(xForce * attackDirection * moveToAttacker * 3 * multiplier, yForce);
		}
		else {
			fly = new Vector2 (xForce * attackDirection * multiplier, yForce*multiplier);
		}
		rigidbody2D.AddForce(fly);
		staggered = true;

	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		direction *= -1;
	}
	
	void Grab() {
		if (airborne) {
			rigidbody2D.AddForce (new Vector2 (grabForceAirMultiple*grabForce*direction, 0f));
		}
		else {
				rigidbody2D.AddForce (new Vector2 (grabForce*direction, 0f));
				
		}
	}
}
