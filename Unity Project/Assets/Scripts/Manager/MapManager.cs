using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour
{

	public float timeBeforeResetNewGame = 10f;
	float _mapTime = 300f;

	public GameObject passiveCamera;
	public GameObject madScientistRespawnSpot;
	public GameObject[] nephewRespawnSpots;
	public GameObject[] nephews;
	public GameObject madScientist;
	public GameObject[] machines;
	public GameObject[] intactMachines;

	public int intactMachineNum = 0;
	public int repairedMachineNum = 0;
	public int destroyedMachineNum = 0;

	bool machinesCalculation = true;

	public float mapTime {
		get {
			return _mapTime;
		}
	}

	public bool hasReset = false;

	void Update ()
	{
		//TODO: if it's server calculate the map time and other settings

		passiveCamera = GameObject.Find ("Main Camera");
		madScientistRespawnSpot = GameObject.FindGameObjectWithTag ("MadScientistRespawnSpot");
		madScientist = GameObject.FindGameObjectWithTag ("MadScientist");
		nephewRespawnSpots = GameObject.FindGameObjectsWithTag ("NephewRespawnSpot");
		nephews = GameObject.FindGameObjectsWithTag ("Nephew");
		machines = GameObject.FindGameObjectsWithTag ("Machine");

		if (hasReset) {
			_mapTime -= Time.deltaTime;
			if (_mapTime <= 0) {
				if (machinesCalculation) {
					MachineCounter ();
					machinesCalculation = false;
				}

				timeBeforeResetNewGame -= Time.deltaTime;
				if (timeBeforeResetNewGame <= 0) {
					timeBeforeResetNewGame = 10f;
					ResetMap ();
				}
			}

		}

	}

	void OnGUI(){
		if (mapTime <= 0) {
			GUILayout.Label (" ");
			GUILayout.Label ("Map will be restart in " + Mathf.Round (timeBeforeResetNewGame).ToString () + " seconds");

			GUILayout.Label ("Intact Machines: " + intactMachineNum.ToString ());
			GUILayout.Label ("Repaired Machines: " + repairedMachineNum.ToString ());
			GUILayout.Label ("Destroyed Machines: " + destroyedMachineNum.ToString ());


			if (destroyedMachineNum > (repairedMachineNum + intactMachineNum)) {
				GUILayout.Label ("Nephews Win");
			} else {
				GUILayout.Label ("Mad Scientist Wins");
			}

		} else {
			GUILayout.Label (" ");
			GUILayout.Label ("Time: " + Mathf.Round (mapTime).ToString ());	
		}
	}

	[PunRPC]
	public void MachineCounter ()
	{
		foreach (GameObject machine in machines) {
			if (machine.GetComponent<MachineControl> ().MachineCurrentStatus == MachineControl.MachineStatus.Intact) {
				intactMachineNum++;
			} else if (machine.GetComponent<MachineControl> ().MachineCurrentStatus == MachineControl.MachineStatus.Repaired) {
				repairedMachineNum++;
			}
		}
		destroyedMachineNum = machines.Length - (repairedMachineNum + intactMachineNum);
	}

	//when a new player enters the map, this method will be invoked and restarts everything in the map
	[PunRPC]
	public void ResetMap ()
	{
		//map time starts over
		hasReset = true;
		machinesCalculation = true;
		_mapTime = 300f;
		timeBeforeResetNewGame = 10f;

		intactMachineNum = 0;
		repairedMachineNum = 0;
		destroyedMachineNum = 0;

		//set all machines to intact
		if (machines != null) {
			foreach (GameObject machine in machines) {
				machine.GetComponent<PhotonView> ().RPC ("RestartMachine", PhotonTargets.AllBuffered);
			}
		}

		//sends mad scientist to his spot
		if (madScientist != null && madScientistRespawnSpot != null) {
			madScientist.transform.position = madScientistRespawnSpot.transform.position;
		}

		//Send all nephews to their respawn spot
		if (nephews != null && nephewRespawnSpots != null) {
			if (nephews.Length <= nephewRespawnSpots.Length) {
				foreach (GameObject nephew in nephews) {
					int respawnNumber = Random.Range (0, nephewRespawnSpots.Length);
					int counter = 0;
					while (nephewRespawnSpots [respawnNumber].GetComponent<SpawnSpot> ().isOccupied) {
						counter++;
						if (counter <= nephewRespawnSpots.Length) {
							if (respawnNumber == Random.Range (0, nephewRespawnSpots.Length)) {
								respawnNumber = Random.Range (0, nephewRespawnSpots.Length);
								//Debug.Log ("R: " + respawnNumber);
							}
						} else {
							break;
						}
					} 
					nephew.transform.position =	nephewRespawnSpots [respawnNumber].transform.position;
				}
			}
		}
	}
}




