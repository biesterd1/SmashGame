using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour {
	public static Transform selfTransform;
	public static GameObject target;
	public static GameObject self;
	public static Transform targetInstance;
	Collider2D targett;

	Vector2 fly;
	Vector2 selfFly;
	public static PlayerControl hitTarget;
	public static PlayerControl player;

	// These values are set in the inspector, per attack
	public float xForce;
	public float yForce;
	public float damage;
	public float selfXForce;
	public float selfYForce;
	public float attackResetTime;
	public int attackDirection = 1;

	public static float attackTimer;


	// Use this for initialization. On enable runs everytime the gameobject is turned on (in animation)
	void OnEnable () {
		// Finds parent of hitbox
		selfTransform = transform.parent.parent;
		self = selfTransform.gameObject;
		player = self.GetComponent<PlayerControl>();

		if (player.facingRight == false) {
			//xForce *= -1;	
			selfXForce *=  -1;
			attackDirection = -1;
		}
		else {
			attackDirection = 1;
		}

		fly = new Vector2 (xForce, yForce);
		selfFly = new Vector2 (selfXForce, selfYForce);

		// Triggers a force on player using attack if necessary
		if (selfXForce != 0 || selfYForce != 0) {
			SelfForces ();
		}

		if (selfXForce < 0) {
			selfXForce *= -1;
		}
		Debug.Log("Self for hitbox is " + self);
	}
	
	// Update is called once per frame
	void Update () {
		attackTimer += Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other) {
		// Checks if target is a player, and not self
		// sets target variable to target and starts Damage function
		targett = other;
		if (other.gameObject.tag == "Player" && other.gameObject != selfTransform /*&& attackTimer >= attackResetTime*/) {
			attackTimer = 0;
			targetInstance = targett.transform;
			target = targetInstance.gameObject;
			hitTarget = target.GetComponent<PlayerControl>();
			Damage(targetInstance);
			// This will need to be fixed to accomodate for more than 2 players. 
			// No idea how.
			// Setting the script of target player to activate "staggered" variable for being hit
			//player2 = target.GetComponent<Player2Control>();

			Debug.Log ("Target instance is " + targetInstance);
		}
	}
	void Damage (Transform target) {
		// Adds force to target
		// Make sure to multiply "fly" by damage percentage of target eventually.

	
		//targetInstance.rigidbody2D.AddForce (fly);
		hitTarget.OnHit(xForce, yForce, damage, attackDirection);
		

	}

	void SelfForces() {
	
			player.rigidbody2D.AddForce(selfFly);
		
	}
}
