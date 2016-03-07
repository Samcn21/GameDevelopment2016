﻿using UnityEngine;
using System.Collections;

public class MadScientistController: MonoBehaviour {

	public float shrinkerRayFireRate = 5f;
	public float shrinkerRaycooldown = 0f;

	public float forceDomeFireRate = 6f;
	public float forceDomeRaycooldown = 0f;

	public float teleporterExileFireRate = 5f;
	public float teleporterExilecooldown = 0f;

	public float freezeRayFireRate = 5f;
	public float freezeRayCooldown = 0f;

	public string currentWeaponName;
	ArrayList inputKeys; 

	void Start () {
		inputKeys = new ArrayList ();
		inputKeys.Add (KeyCode.Alpha1);
		inputKeys.Add (KeyCode.Alpha2);
		inputKeys.Add (KeyCode.Alpha3);
		inputKeys.Add (KeyCode.Alpha4);
		inputKeys.Add (KeyCode.Alpha5);
		inputKeys.Add (KeyCode.Alpha6);
	}

	[PunRPC]
	void Update () {
		shrinkerRaycooldown -= Time.deltaTime;
		forceDomeRaycooldown -= Time.deltaTime;

		foreach (KeyCode key in inputKeys){
			if (Input.GetKeyDown(key)){
				switch (key) 
				{
				case KeyCode.Alpha1:
					currentWeaponName = "Shrinker Ray";
					this.GetComponent<PhotonView> ().RPC ("SwitchWeapons", PhotonTargets.All, currentWeaponName);
					//SwitchWeapons (currentWeaponName);
					break;
				case KeyCode.Alpha2:
					currentWeaponName = "Force Dome";
					this.GetComponent<PhotonView> ().RPC ("SwitchWeapons", PhotonTargets.All, currentWeaponName);
					SwitchWeapons (currentWeaponName);
					break;
				case KeyCode.Alpha3:
					currentWeaponName = "Teleporter Exile";
					this.GetComponent<PhotonView> ().RPC ("SwitchWeapons", PhotonTargets.All, currentWeaponName);
					SwitchWeapons (currentWeaponName);
					break;
				case KeyCode.Alpha4:
					currentWeaponName = "Freeze Ray";
					this.GetComponent<PhotonView> ().RPC ("SwitchWeapons", PhotonTargets.All, currentWeaponName);
					SwitchWeapons (currentWeaponName);
					break;
				case KeyCode.Alpha5:
					currentWeaponName = "Jetpack";
					this.GetComponent<PhotonView> ().RPC ("SwitchWeapons", PhotonTargets.All, currentWeaponName);
					SwitchWeapons (currentWeaponName);
					break;
				case KeyCode.Alpha6:
					currentWeaponName = "Mind Swap Ray";
					this.GetComponent<PhotonView> ().RPC ("SwitchWeapons", PhotonTargets.All, currentWeaponName);
					SwitchWeapons (currentWeaponName);
					break;
				default:
					break;
				}
			}
		}

		switch (currentWeaponName) {
		case "Shrinker Ray":
			if (Input.GetButton("Fire1")){
				Shoot(shrinkerRaycooldown, shrinkerRayFireRate, currentWeaponName);
			}
			break;
		case "Force Dome":
			if (Input.GetButton("Fire1")){
				Shoot(forceDomeRaycooldown, forceDomeFireRate, currentWeaponName);
			}
			break;
		case "Teleporter Exile":
			if (Input.GetButton("Fire1")){
				Shoot(teleporterExilecooldown, teleporterExileFireRate, currentWeaponName);
			}
			break;		
		case "Freeze Ray":
			if (Input.GetButton("Fire1")){
				Shoot(freezeRayCooldown, freezeRayFireRate, currentWeaponName);
			}
			break;
		default:
			//Debug.Log ("I have nothing!!!");
			break;
		}

	}

	[PunRPC]
	void SwitchWeapons(string whichWeapon) {
		foreach(Transform child in transform){
			if (child.tag == "MSWeapon"){
				if (child.name == whichWeapon) {
					child.gameObject.SetActive(true);
				}
				else
				{
					child.gameObject.SetActive(false);
				}
			}
		}
	}

	void Shoot(float cooldown, float fireRate, string weaponName){
		if (cooldown > 0){
			return;
		}
		Debug.Log ("shooting: " + weaponName);
		Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
		Transform hitTransform;
		Vector3 hitPoint;

		hitTransform = FindShotObjects(ray, out hitPoint);

		if (hitTransform != null){
			//special effect in hit location - hitinfo.point

			Debug.Log ("I hit:" + hitTransform.GetComponent<Collider>().name);
			RayReciever rr = hitTransform.transform.GetComponent<RayReciever>();

			if (rr != null){
				//rr.TakeHit (120, "Shrinker Ray");
				rr.GetComponent<PhotonView> ().RPC ("TakeHit", PhotonTargets.All, 120, weaponName);
			}
		}

		//if (Physics.Raycast(ray, out hitInfo)) {
		//	Debug.Log ("I hit:" + hitInfo.collider.name);
		//}
		cooldown = fireRate;
	}

	Transform FindShotObjects(Ray ray, out Vector3 hitPoint){
		RaycastHit[] hitObjects = Physics.RaycastAll(ray);

		Transform closestHit = null;
		float closestHitDist = 0f;
		hitPoint = Vector3.zero;

		foreach (RaycastHit hitObject in hitObjects){
			if ((hitObject.transform != this.transform) && (closestHit == null || hitObject.distance < closestHitDist)) {
				closestHit = hitObject.transform;
				closestHitDist = hitObject.distance;
				hitPoint = hitObject.point;
			}
		}
		return closestHit;
	}
}
