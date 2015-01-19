using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
namespace CompleteProject {

	public class DebugUI : MonoBehaviour
	{
		Text text;
		public static GameObject self;
		float timer = 0;
		public static GameMaster gm;


		// Use this for initialization
		void Awake ()
		{
			if (gm == null) {
				gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
			}
			text = GetComponent <Text> ();
		}
		
		// Update is called once per frame
		void Update ()
		{
			timer += Time.deltaTime;
			text.text = "Player 1 h = " + gm.playerObjects[0].GetComponent<PlayerControl>().h;
//			text.text = "v = " + PlayerControl.v + ",  h = " + PlayerControl.h + "\nKinematic = " + PlayerControl.kinematic + ", Timer = " + PlayerControl.timer1
//				 + "\nSpeed = " + PlayerControl.xSpeed + "\nMax Speed = " + PlayerControl.maxSpeed2 + 
//					"\nParent = " + Hitbox.parent + "Target = " + Hitbox.target + "\nStaggered = " + PlayerControl.staggered;
		}
	}
	
}