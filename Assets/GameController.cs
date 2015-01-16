using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static int numberOfPlayers = 2;
	public static Transform[] spawns;				// Set spawn locations. Array for multiple possibilities
	public static Transform spawn;
	public static GameObject[] spawnObjects;
	static int spawnIndex;




	// Use this for initialization
	void Start () {
		// Allows players to move through each other
		Physics2D.IgnoreLayerCollision(8, 8, true);
		spawnIndex = 0;
		spawnObjects = GameObject.FindGameObjectsWithTag("Spawn");
		for (int i=0;i<spawnObjects.Length;i++) {
			spawns[i] = spawnObjects[i].transform;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public static void KillPlayer(Player player, GameObject playerObject) {

		GameController.SpawnPlayer(player, playerObject);

	}

	public static void SpawnPlayer(Player player, GameObject playerObject) {
		Debug.Log ("Spawn()");
		spawn = spawns[spawnIndex];
		spawnIndex += 1;
		if (spawnIndex == spawnObjects.Length) spawnIndex = 0;

		playerObject.rigidbody2D.position = spawn.transform.position;
		playerObject.rigidbody2D.velocity = new Vector2(0,0);				// Resets momentum
//		rigidbody2D.isKinematic = true;							// Prevents forces from acting on it so player freezes (like gravity)
//		time1Start = true;
//		spawning = true;
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
