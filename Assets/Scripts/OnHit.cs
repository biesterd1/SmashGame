using UnityEngine;
using System.Collections;

public class OnHit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Hit() {
		PlayerControl.staggered = true;
	}
}
