using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;

	// Use this for initialization
	void Start () {
	
	}

	void Update () {
		//PhotonView
		if (photonView.isMine) {
			//do Nothing - cause character is mine
		} else {
				transform.position = Vector3.Lerp (transform.position, realPosition, 0.1f);
				transform.rotation = Quaternion.Lerp (transform.rotation, realRotation, 0.1f);
			}
	}
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		//Debug.Log("Event exist!");
		if (stream.isWriting) {
			//Our player in network. Sending out our actual position to the network
			stream.SendNext(transform.position);
			stream.SendNext (transform.rotation);
		} else {
			//Other player in network. recieving their actual position to the network
			realPosition = (Vector3)stream.ReceiveNext();
			realRotation = (Quaternion)stream.ReceiveNext ();
		}
	}
}
