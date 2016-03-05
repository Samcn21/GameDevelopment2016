using UnityEngine;
using System.Collections;

public class MachineControl : Photon.MonoBehaviour {

	public Material brokenMachine;
	public float destroyTime = 4f;
	public Material fixedMachine;
	public float fixedTime = 2f;
	//Vector3 realPosition = Vector3.zero;
	//Quaternion realRotation = Quaternion.identity;
	//public renderer rend;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay (Collider other) {
		if (other.tag == "MadScientist") {
			if (Input.GetMouseButton(0)){
				fixedTime -= Time.deltaTime;
				if (fixedTime < 0) {
					//GetComponent<Renderer> ().material = fixedMachine;
					transform.position = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
					fixedTime = 2f;
				}
			}
		} 
		else if (other.tag == "Nephew") {
			if (Input.GetMouseButton(0)){
				destroyTime -= Time.deltaTime;
				if (destroyTime < 0) {
					//GetComponent<Renderer> ().material = brokenMachine;
					transform.position = new Vector3 (transform.position.x, transform.position.y - 1, transform.position.z);
					destroyTime = 4f;
				}
			}
		}
	}

}
