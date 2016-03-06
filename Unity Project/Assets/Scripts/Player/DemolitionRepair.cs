using UnityEngine;
using System.Collections;

public class DemolitionRepair : MonoBehaviour {
	public Material brokenMachine;
	public float demolitionTime = 2f;
	public Material repairedMachine;
	public float repairTime = 1f;
	public MachineControl mc;
	// Use this for initialization
	void Start () {
		//MachineControl mc = 
	}

	void OnTriggerEnter(Collider other){
		if (other.name == "Machine"){
			//Debug.Log (other);
			mc = other.GetComponent<MachineControl>();
			mc.GetComponent<PhotonView> ().RPC ("CallMe",PhotonTargets.All);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey(KeyCode.E)){
			Debug.Log ("Repairing Machine");

			//MachineControl mc = transform.GetComponent<MachineControl>();
			//mc.GetComponent<PhotonView> ().RPC ("OnTriggerStay", PhotonTargets.All, );

		}
	}
}
