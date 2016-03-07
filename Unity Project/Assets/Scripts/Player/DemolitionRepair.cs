using UnityEngine;
using System.Collections;

public class DemolitionRepair : MonoBehaviour {
	public Material brokenMachine;
	public float demolitionTime = 2f;
	public Material repairedMachine;
	public float repairTime = 1f;
	public bool workingStatus = false;

	public MachineControl mc;
	public string characterTag = "";
	// Use this for initialization
	void Start () {
		characterTag = transform.tag.ToString ();
		Debug.Log (characterTag);
	}

	void OnTriggerEnter(Collider other){
		if (other.name == "Machine"){
			mc = other.GetComponent<MachineControl>();
			mc.GetComponent<PhotonView> ().RPC ("DemolitionRepair",PhotonTargets.All, characterTag);

			mc.GetComponent<PhotonView> ().RPC ("WorkingPermissionFlag", PhotonTargets.All);
		}

	}

		// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.E) && workingStatus){
			//we need to send the destroying/repairing status to the MachineControl script.
			//we need to disable the player's control of movement as soon as player hold E to fix or destroy
			if (characterTag == "MadScientist") {
				// fixing counter
				//animation of fixing
			}else if(characterTag == "Nephew") {
				//animation of destroying
			}

			//Debug.Log ("Repairing Machine");
		}
	}
}
