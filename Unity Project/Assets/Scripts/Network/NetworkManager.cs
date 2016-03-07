using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour {

	public bool offlineMode = false;
	public GameObject passiveCamera;
	GameObject madScientistRespawnSpot;
	GameObject[] nephewRespawnSpots;
	GameObject nephewRespawnSpot;
	public GameObject[] machineSpawnSpots;

	void Start () {
		passiveCamera = GameObject.Find ("Main Camera");
		madScientistRespawnSpot = GameObject.FindGameObjectWithTag("MadScientistRespawnSpot");
		nephewRespawnSpots = GameObject.FindGameObjectsWithTag("NephewRespawnSpot");
		nephewRespawnSpot = GameObject.FindGameObjectWithTag("NephewRespawnSpot");

		//machineSpawnSpots = GameObject.FindGameObjectsWithTag ("MachineSpawnSpot");


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
			Debug.Log(PhotonNetwork.countOfPlayers.ToString ());

			//SpawnMachines ();

		} else {
			switch (PhotonNetwork.countOfPlayers)
			{
			case 2:
				Debug.Log(PhotonNetwork.countOfPlayers.ToString ());
				RespawnNephew (0);
				break;
			case 3:
				Debug.Log(PhotonNetwork.countOfPlayers.ToString ());
				RespawnNephew (1);
				break;
			case 4:
				Debug.Log(PhotonNetwork.countOfPlayers.ToString ());
				RespawnNephew (2);
				break;
			}
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
		//madScientistControllerGO.transform.FindChild ("Weapons").gameObject.SetActive(true);

		((MonoBehaviour)madScientistControllerGO.GetComponent("FirstPersonController")).enabled = true;
		((MonoBehaviour)madScientistControllerGO.GetComponent("DemolitionRepair")).enabled = true;

}
	void RespawnNephew (int playerNumber){
		GameObject nephewControllerGO = (GameObject)PhotonNetwork.Instantiate ("Nephew" , nephewRespawnSpots[playerNumber].transform.position, nephewRespawnSpot.transform.rotation, 0);
		nephewControllerGO.name = nephewControllerGO.name + playerNumber.ToString ();
		Debug.Log (nephewControllerGO.name);
		nephewControllerGO.transform.FindChild ("FirstPersonCharacter").gameObject.SetActive (true);
		((MonoBehaviour)nephewControllerGO.GetComponent("FirstPersonController")).enabled = true;
		((MonoBehaviour)nephewControllerGO.GetComponent("RayReciever")).enabled = true;
		((MonoBehaviour)nephewControllerGO.GetComponent("NephewController")).enabled = true;
		((MonoBehaviour)nephewControllerGO.GetComponent("DemolitionRepair")).enabled = true;

	}

	void SpawnMachines(){
		foreach(GameObject machine in  machineSpawnSpots){
			if (machine != null) {
				GameObject MachineControllerGO = (GameObject)PhotonNetwork.Instantiate ("Machine", machine.transform.position, machine.transform.rotation, 0);
			}
		}
	}
}
