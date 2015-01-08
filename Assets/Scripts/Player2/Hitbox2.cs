using UnityEngine;
using System.Collections;

public class Hitbox2 : MonoBehaviour {
	public static Transform parent;
	public static Transform target;
	[SerializeField] float xForce = 0;
	[SerializeField] float yForce = 0;
	Vector3 fly;

	// Use this for initialization
	void Start () {
		// Finds parent of hitbox
		parent = transform.parent;
		fly = new Vector3 (xForce, yForce, 0);
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter2D(Collider2D other) {
		// Checks if target is a player, and not self
		// sets target variable to target and starts Damage function
		if (other.gameObject.tag == "Player" && other.gameObject != parent) {
			target = other.transform;
			Damage(target);
		}
	}
	void Damage (Transform target) {
		// Adds force to target
		// Make sure to multiply "fly" by damage percentage of target eventually.
		// Fix direction of force
		target.rigidbody2D.AddForce(fly);
	}
}
