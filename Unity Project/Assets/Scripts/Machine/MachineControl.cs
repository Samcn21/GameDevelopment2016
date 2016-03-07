using UnityEngine;
using System.Collections;

public class MachineControl : Photon.MonoBehaviour {

	public Material brokenMachine;
	public float destroyTime = 2f;
	public Material fixedMachine;
	public float fixedTime = 1f;
	public string colliderName = "";
	public bool workingPermission = false;
	public enum MachineStatus
	{
		Intact, Destroyed, Repaired
	}
	//Vector3 realPosition = Vector3.zero;
	//Quaternion realRotation = Quaternion.identity;
	//public renderer rend;
	// Use this for initialization
	void Start () {
		MachineStatus MachineCurrentStatus = MachineStatus.Intact;

		//if someone stay in a area close to the machine it means he can start destroying or repairing and
		// the permission for working will be true 
		workingPermission = false;

		Debug.Log (MachineCurrentStatus);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("machine status: " + workingStatus);

		if (colliderName != "") {
			//Debug.Log (colliderName);

		}

	}

	[PunRPC]
	public void WorkingPermissionFlag(){
		Debug.Log(workingPermission);
		if (workingPermission) {
			
		}
	}

	[PunRPC]
	public void DemolitionRepair (string characterType) {
		if (characterType == "MadScientist") {
			//Repair
			// after all MachineStatus MachineCurrentStatus = MachineStatus.Repaired;
			//Debug.Log ("character type: mad scientist / repair");
		} 
		else if (characterType == "Nephew") {
			//Demolition
			// after all MachineStatus MachineCurrentStatus = MachineStatus.Destroyed;
			//Debug.Log ("character type: nephew / demolition");

		}

	}
		
	public void OnTriggerExit (Collider other) {
		workingPermission = false;
	}

	public void OnTriggerStay (Collider other) {
		if (other.tag == "MadScientist"){
			colliderName = "MadScientist";
			workingPermission = true;
			//Debug.Log ("mad scientist on trigger: " + workingPermission);
		}
		else if (other.tag == "Nephew") {
			colliderName =  "Nephew";
			workingPermission = true;
			//Debug.Log ("nephew on trigger: " + workingPermission);
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
