using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour {

	public bool offlineMode = false;
	public GameObject passiveCamera;
	GameObject madScientistRespawnSpot;
	GameObject nephewRespawnSpot;
	public GameObject[] machineSpawnSpots;

	void Start () {
		passiveCamera = GameObject.Find ("Main Camera");
		madScientistRespawnSpot = GameObject.FindGameObjectWithTag("MadScientistRespawnSpot");
		nephewRespawnSpot = GameObject.FindGameObjectWithTag("NephewRespawnSpot");
		machineSpawnSpots = GameObject.FindGameObjectsWithTag ("MachineSpawnSpot");


	}
	void Connection() {
			PhotonNetwork.ConnectUsingSettings ("Prototype_3");
	}
	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
		GUILayout.Label ("in room:" + PhotonNetwork.inRoom.ToString ());

		if (PhotonNetwork.connected == false) {
			if (GUILayout.Button ("Single Player")) {
				PhotonNetwork.offlineMode = true;
				OnJoinedLobby ();
				SpawnMachines ();
			} 
			if (GUILayout.Button ("Multi Player")) {
				Connection ();
			}
		
		}

	}
	void OnJoinedLobby(){
		//PhotonNetwork.CreateRoom ("F1");
		PhotonNetwork.JoinRandomRoom ();
		Debug.Log ("OnJoinRoom");
	}
	void OnConnectedToMaster(){
		Debug.Log ("connected");
		PhotonNetwork.JoinLobby ();
	}
	void OnPhotonRandomJoinFailed (){
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null);
		//RoomOptions roomOptions = new RoomOptions () { isVisible = true, maxPlayers = 2 };
	}
	void OnPhotonJoinedRoomFailed(){
		Debug.Log ("OnPhotonJoinedRoomFailed");
	}
	void OnJoinedRoom (){
		Debug.Log ("Joined");
		passiveCamera.SetActive (false);

	
		if (PhotonNetwork.isMasterClient) {
			RespawnMadScientist ();
			//SpawnMachines ();

		} else {
			RespawnNephew ();
		}



//		if (GameObject.FindGameObjectWithTag("MadScientist") == null) {
//			Debug.Log ("Not Found MadScientist");
//
//		} else {
//			Debug.Log ("Found MadScientist");
//		}

	}
	void RespawnMadScientist (){
		
		GameObject madScientistControllerGO = (GameObject)PhotonNetwork.Instantiate ("MadScientist", madScientistRespawnSpot.transform.position, madScientistRespawnSpot.transform.rotation, 0);
		madScientistControllerGO.transform.FindChild ("FirstPersonCharacter").gameObject.SetActive (true);
		((MonoBehaviour)madScientistControllerGO.GetComponent("FirstPersonController")).enabled = true;

}
	void RespawnNephew (){
		GameObject nephewControllerGO = (GameObject)PhotonNetwork.Instantiate ("Nephew", nephewRespawnSpot.transform.position, nephewRespawnSpot.transform.rotation, 0);
		nephewControllerGO.transform.FindChild ("FirstPersonCharacter").gameObject.SetActive (true);
		((MonoBehaviour)nephewControllerGO.GetComponent("FirstPersonController")).enabled = true;
	}

	void SpawnMachines(){
		foreach(GameObject machine in  machineSpawnSpots){
			if (machine != null) {
				GameObject MachineControllerGO = (GameObject)PhotonNetwork.Instantiate ("Machine", machine.transform.position, machine.transform.rotation, 0);
			}
		}
	}
}
