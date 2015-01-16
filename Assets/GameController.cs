using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static int numberOfPlayers = 2;



	// Use this for initialization
	void Start () {
		// Allows players to move through each other
		Physics2D.IgnoreLayerCollision(8, 8, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void KillPlayer(Player player) {
		Destroy (player.gameObject);
		SpawnPlayer (player);

	}

	public static void SpawnPlayer(Player player) {
		Debug.Log ("Spawn()");
		rigidbody2D.position = spawn.transform.position;
		rigidbody2D.velocity = new Vector2(0,0);				// Resets momentum
		rigidbody2D.isKinematic = true;							// Prevents forces from acting on it so player freezes (like gravity)
		time1Start = true;
		spawning = true;
	}
}
