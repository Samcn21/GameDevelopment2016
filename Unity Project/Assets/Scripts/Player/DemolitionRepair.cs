using UnityEngine;
using System.Collections;

public class DemolitionRepair : MonoBehaviour {
	
	public float demolitionTime = 3f;
	public float repairTime = 1.5f;

	public bool isRepaired = false;
	public bool isDestroyed = false;
	public bool oneTimeCall = false;

	GameObject[] allMachines;
	public MachineControl mc;
	public string characterTag = "";

	void Start () {
		characterTag = transform.tag.ToString ();
		allMachines = GameObject.FindGameObjectsWithTag ("Machine");
	}

	void OnTriggerStay(Collider other){
		//if (other.name == "Machine"){
			if (oneTimeCall){
				mc = other.GetComponent<MachineControl>();
				mc.GetComponent<PhotonView> ().RPC ("DemolitionRepair",PhotonTargets.All, characterTag, isRepaired, isDestroyed);
				oneTimeCall = false;
				//Debug.Log ("Invoked Demolition Repair function in Machine Control script");				
			}
		//}
	}

	void Update () {
		//we need to disable the player's control of movement as soon as player hold E to fix or destroy
		//not to let the player move during repairing or destroying
		if (Input.GetKey(KeyCode.E)){
			if (characterTag == "MadScientist")  {
				repairTime -= Time.deltaTime;
				if (repairTime < 0) {
					//animation of fixing
					isRepaired = true;
					oneTimeCall = true;
					repairTime = 1.5f;
				}
			}else if(characterTag == "Nephew") {
				demolitionTime -= Time.deltaTime;
				if (demolitionTime < 0) {
					//animation of destroying
					isDestroyed = true;
					oneTimeCall = true;
					demolitionTime = 3f;
				}
			}
		}
	}
}
