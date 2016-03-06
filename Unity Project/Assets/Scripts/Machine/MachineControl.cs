using UnityEngine;
using System.Collections;

public class MachineControl : Photon.MonoBehaviour {

	public Material brokenMachine;
	public float destroyTime = 2f;
	public Material fixedMachine;
	public float fixedTime = 1f;
	public string colliderName = "";
	//Vector3 realPosition = Vector3.zero;
	//Quaternion realRotation = Quaternion.identity;
	//public renderer rend;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (colliderName != "") {
			//Debug.Log (colliderName);
		}
	}

	[PunRPC]
	public void CallMe (){
		Debug.Log ("It's calling me....................");
	}

	[PunRPC]
	public void OnTriggerStay (Collider other) {

		if (other.tag == "MadScientist"){
			colliderName = "MadScientist";
		}
		else if (other.tag == "Nephew") {
			colliderName =  "Nephew";
		}


		if (other.tag == "MadScientist") {
			if (Input.GetMouseButton(0)){
				fixedTime -= Time.deltaTime;
				if (fixedTime < 0) {
					GetComponent<Renderer> ().material = fixedMachine;
					transform.position = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
					fixedTime = 1f;
				}
			}
		} 
		else if (other.tag == "Nephew") {
			if (Input.GetMouseButton(0)){
				destroyTime -= Time.deltaTime;
				if (destroyTime < 0) {
					GetComponent<Renderer> ().material = brokenMachine;
					transform.position = new Vector3 (transform.position.x, transform.position.y - 1, transform.position.z);
					destroyTime = 2f;
				}
			}
		}
	}

}
