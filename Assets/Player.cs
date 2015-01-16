using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public class PlayerStats {
		public int health = 0f;
	}

	public PlayerStats playerStats = new PlayerStats();


	// kills player if he leaves boundary
	void OnCollisionEnter2D(Collision2D coll) {
		
		if (coll.gameObject.tag == "Boundary") {						// if Player hits boundary, trigger respawn
			Debug.Log ("Hit boundary");	
			GameController.KillPlayer (this);
			
		}
	}
	public void DamagePlayer (int damage) {
		playerStats.health -= damage;
		if (playerStats.health >= 200) {
			GameController.KillPlayer(this);
		}
	}
}
