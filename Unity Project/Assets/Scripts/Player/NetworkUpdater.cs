using UnityEngine;
using System.Collections;

public class NetworkUpdater : MonoBehaviour {
	MapManager mm;

	// Use this for initialization
	void Start () {
		mm = GameObject.FindGameObjectWithTag ("GlobalScript").GetComponent <MapManager> ();
		if (mm != null) {
			
			//when a new player enters the map, we need to invoke ResetMap method in MapManager class
			mm.GetComponent<PhotonView> ().RPC ("ResetMap", PhotonTargets.AllBuffered);
		}
	}
	
	void Update () {
		
	}
}
