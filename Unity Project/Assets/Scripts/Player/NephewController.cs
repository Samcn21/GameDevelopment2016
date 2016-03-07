using UnityEngine;
using System.Collections;

public class NephewController: MonoBehaviour {
	public float disableTime = 5f;
	public bool timeStart = false;
	// Use this for initialization
	void Start () {
		
	}



//	void Update () {
//		if (timeStart) {
//			disableTime -= Time.deltaTime;
//			if (disableTime < 0) {
//				((MonoBehaviour)gameObject.GetComponent("FirstPersonController")).enabled = true;
//				timeStart = false;
//				disableTime = 5f;
//			}
//		}
//	}

//	void OnTriggerEnter (Collider other){
//		if (other.tag == "MadScientist")
//		{
//			Debug.Log ("You Caught me!!!");
//			timeStart = true;
//			((MonoBehaviour)gameObject.GetComponent("FirstPersonController")).enabled = false;
//		}
//	}
}
