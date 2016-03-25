using UnityEngine;
using System.Collections;

public class NephewController: MonoBehaviour {
	// Use this for initialization
	void Start () {
		

	}



	void Update () {
//		if (Input.GetKeyDown (KeyCode.R)) {
//			//find all nephews in the scene
//			allNephews = GameObject.FindGameObjectsWithTag ("Nephew");
//
//			//Mind Swap allowed (We have at least 2 nephews in the scene)
//			//if (allNephews.Length >= 2){
//				foreach (GameObject eachNephew in allNephews){
//					//We found another nephew
//					if (eachNephew.gameObject != this.gameObject){
//
//
//						myCameraController = this.transform.FindChild("FirstPersonCharacter").gameObject;
//						GameObject myCameraTemp = (GameObject)PhotonNetwork.Instantiate (myCameraController.name, Vector3.zero, Quaternion.identity, 0);
//
//						anotherCameraController = eachNephew.transform.FindChild("FirstPersonCharacter").gameObject;
//						GameObject anotherCameraTemp = (GameObject)PhotonNetwork.Instantiate (anotherCameraController.name, Vector3.zero, Quaternion.identity, 0);
//
//						myCameraTemp.transform.parent = anotherCameraTemp.transform.parent;
//
//						
//						//anotherCameraController.transform.gameObject = myCameraController.transform.gameObject;
//						//myCameraController.transform.gameObject = myCameraTemp.transform.gameObject;
//						
//						//eachNephew.transform.parent = this.transform.FindChild("FirstPersonCharacter").gameObject.transform;
//						//anotherCameraController = myCameraController;
//					}
//				}
//			//}
//		}



//		if (timeStart) {
//			disableTime -= Time.deltaTime;
//			if (disableTime < 0) {
//				((MonoBehaviour)gameObject.GetComponent("FirstPersonController")).enabled = true;
//				timeStart = false;
//				disableTime = 5f;
//			}
//		}
	}

//	void OnTriggerEnter (Collider other){
//		if (other.tag == "MadScientist")
//		{
//			Debug.Log ("You Caught me!!!");
//			timeStart = true;
//			((MonoBehaviour)gameObject.GetComponent("FirstPersonController")).enabled = false;
//		}
//	}
}
