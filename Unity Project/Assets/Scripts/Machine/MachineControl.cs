using UnityEngine;
using System.Collections;

public class MachineControl : Photon.MonoBehaviour {

	public Material intactMachine;
	public Material destroyedMachine;
	public Material repairedMachine;

	public enum MachineStatus
	{
		Intact, Destroyed, Repaired
	}
	public MachineStatus MachineCurrentStatus;

	void Start () {
		MachineCurrentStatus = MachineStatus.Intact;
		GetComponent<Renderer> ().material = intactMachine;
	}
	
	[PunRPC]
	public void DemolitionRepair (string characterType, bool isRepaired, bool isDestroyed) {
		if (characterType == "MadScientist" && isRepaired) {
			if (MachineCurrentStatus == MachineStatus.Destroyed){
				MachineCurrentStatus = MachineStatus.Repaired;
				GetComponent<Renderer> ().material = repairedMachine;
				// after all MachineStatus MachineCurrentStatus = MachineStatus.Repaired;
				//Debug.Log ("character type: mad scientist / repair");
			}
		} 
		else if (characterType == "Nephew" && isDestroyed) {
			if (MachineCurrentStatus == MachineStatus.Repaired || MachineCurrentStatus == MachineStatus.Intact){
				MachineCurrentStatus = MachineStatus.Destroyed;
				GetComponent<Renderer> ().material = destroyedMachine;
				// after all MachineStatus MachineCurrentStatus = MachineStatus.Destroyed;
				//Debug.Log ("character type: nephew / demolition");
			}
		}

	}

}
