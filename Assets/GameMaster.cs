using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

	public static int numberOfPlayers = 2;
	public static Transform[] spawns;				// Set spawn locations. Array for multiple possibilities
	public static Transform spawn;
	public static GameObject[] spawnObjects;
	public static GameObject[] playerObjectsTemp;
	static int spawnIndex;
	public int spawnTimer = 2;

	public static GameMaster gm;

	public Transform playerPrefab;
	public Transform[] players;
	public Transform[] spawnPoints;

	List<GameObject> playerObjects = new List<GameObject>();

	bool[] playersAlive;

	// Use this for initialization
	void Start () {
//		for (int i = 0; i < players.Length; i++){
//			playersAlive[i] = true;
//		}
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}

		// Populates array of player objects
		playerObjectsTemp = GameObject.FindGameObjectsWithTag("Player");

		// Orders the player objects in a playerObjects List
		for (int i=0;i<numberOfPlayers;i++) {
			for (int j=0;j<numberOfPlayers;j++) {
				int index = (int)playerObjectsTemp[j].GetComponent<PlayerControl>().player;
				if (index == i+1) {
					playerObjects.Add(playerObjectsTemp[i]);
				}
			}

		}

		// Allows players to move through each other
		Physics2D.IgnoreLayerCollision(8, 8, true);

		// Should spawn all players in respective spawn points
		//gm.StartCoroutine(gm.RespawnPlayer(player));

//		spawnIndex = 0;
//		spawnObjects = GameObject.FindGameObjectsWithTag("Spawn");
//		for (int i=0;i<spawnObjects.Length;i++) {
//			spawns[i] = spawnObjects[i].transform;
//		}
		for (int i = 0; i<numberOfPlayers ; i++){
			playerObjects[i].SetActive (false);
		}
		gm.StartCoroutine(gm.RespawnAll());
	}


	
	// Update is called once per frame
	void Update () {

	}

	public static void KillPlayer(Player player) {
		//Destroy(player.gameObject);
		player.gameObject.SetActive(false);
		//gm.SpawnPlayer(player, playerObject);
		gm.StartCoroutine(gm.RespawnPlayer(player));
	
	}

	public static void KillPlayer1 (){

	}

	public IEnumerator RespawnAll () {
		yield return new WaitForSeconds(spawnTimer);
		for (int i = 0; i < numberOfPlayers; i++){
			if (playerObjects[i].activeSelf == false){
				playerObjects[i].SetActive (true);
				playerObjects[i].transform.position = spawnPoints[i].position;
			}
		}
	}

	public IEnumerator RespawnPlayer(Player player) {


		yield return new WaitForSeconds(spawnTimer);

		player.gameObject.SetActive (true);
		player.gameObject.transform.position = spawnPoints[1].position;

//		for (int i = 0; i < players.Length; i++){
//			bool active = players[i].gameObject.activeSelf;
//			if (active == false){
//			
//				Debug.Log("Spawn!");
//			}
//			Debug.Log (players[i].gameObject);
//		}

	}

//	public static void SpawnPlayer(Player player, GameObject playerObject) {
//		Debug.Log ("Spawn()");
//		spawn = spawns[spawnIndex];
//		spawnIndex += 1;
//		if (spawnIndex == spawnObjects.Length) spawnIndex = 0;
//
//		playerObject.rigidbody2D.position = spawn.transform.position;
//		playerObject.rigidbody2D.velocity = new Vector2(0,0);				// Resets momentum
////		rigidbody2D.isKinematic = true;							// Prevents forces from acting on it so player freezes (like gravity)
////		time1Start = true;
////		spawning = true;
//	}
}
