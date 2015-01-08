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
}
