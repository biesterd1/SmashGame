using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace CompleteProject {

	public class DebugUI : MonoBehaviour
	{
		Text text;
		public static GameObject self;
		float timer = 0;


		// Use this for initialization
		void Awake ()
		{

			text = GetComponent <Text> ();
		}
		
		// Update is called once per frame
		void Update ()
		{
			timer += Time.deltaTime;
			text.text = "" + PlayerControl.moveToAttacker;
//			text.text = "v = " + PlayerControl.v + ",  h = " + PlayerControl.h + "\nKinematic = " + PlayerControl.kinematic + ", Timer = " + PlayerControl.timer1
//				 + "\nSpeed = " + PlayerControl.xSpeed + "\nMax Speed = " + PlayerControl.maxSpeed2 + 
//					"\nParent = " + Hitbox.parent + "Target = " + Hitbox.target + "\nStaggered = " + PlayerControl.staggered;
		}
	}
	
}