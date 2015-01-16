using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
<<<<<<< HEAD
	static Player player;
	static GameObject playerObject;

	public class PlayerStats {
		public int health = 0;
=======

	public class PlayerStats {
		public int health = 0f;
>>>>>>> ad9ea3d44bcf4903d08af568cc181d0fd3370f86
	}

	public PlayerStats playerStats = new PlayerStats();


	// kills player if he leaves boundary
	void OnCollisionEnter2D(Collision2D coll) {
<<<<<<< HEAD

		// if Player hits boundary, trigger respawn
		if (coll.gameObject.tag == "Boundary") {	
			Debug.Log ("Hit boundary");	
			GameController.KillPlayer (player, playerObject);
=======
		
		if (coll.gameObject.tag == "Boundary") {						// if Player hits boundary, trigger respawn
			Debug.Log ("Hit boundary");	
			GameController.KillPlayer (this);
>>>>>>> ad9ea3d44bcf4903d08af568cc181d0fd3370f86
			
		}
	}
	public void DamagePlayer (int damage) {
		playerStats.health -= damage;
		if (playerStats.health >= 200) {
<<<<<<< HEAD
			GameController.KillPlayer(player, playerObject);
		}
	}

	void Start() {
		player =  this;
		playerObject = player.gameObject;
		GameController.SpawnPlayer(player, playerObject);

	}
=======
			GameController.KillPlayer(this);
		}
	}
>>>>>>> ad9ea3d44bcf4903d08af568cc181d0fd3370f86
}
