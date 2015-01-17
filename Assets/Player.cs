﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	static Player player;
	static GameObject playerObject;
	static Transform playerPrefab;


	public class PlayerStats {
		public int health = 0;
	}

	void Start() {
		player = this;
		playerObject = player.gameObject;
		//GameMaster.RespawnPlayer();
		
	}

	public PlayerStats playerStats = new PlayerStats();


	// kills player if he leaves boundary
	void OnCollisionEnter2D(Collision2D coll) {

		// if Player hits boundary, trigger respawn
		if (coll.gameObject.tag == "Boundary") {	
			Debug.Log ("Hit boundary");	
			GameMaster.KillPlayer (player, playerObject);
			
		}
	}
	public void DamagePlayer (int damage) {
		playerStats.health -= damage;
		if (playerStats.health >= 200) {
			GameMaster.KillPlayer(player, playerObject);
		}
	}

//	void OnDestroy() {
//		Debug.Log("Destroyed " + player)
//		
//	}


}
