using UnityEngine;
using System.Collections;

public class SpawnSpot : MonoBehaviour {

	public int teamID = 0;
	public bool isOccupied; 

	void Start(){
		isOccupied = true;
	}

	void OnTriggerEnter(Collider other){
		isOccupied = true;
	}
	void OnTriggerStay(Collider other) {
		if (other != null) {
			isOccupied = true;
		} 
	}
	void OnTriggerExit(Collider other){
		isOccupied = false;
	}
}
