using UnityEngine;
using System.Collections;

public class MadScientistController: MonoBehaviour {

	public float shrinkerRayFireRate = 5f;
	public float cooldown = 0f;

	public string currentWeaponName;
	ArrayList inputKeys; 

	void Start () {
		inputKeys = new ArrayList ();
		inputKeys.Add (KeyCode.Alpha1);
		inputKeys.Add (KeyCode.Alpha2);
	}
	
	void Update () {
		cooldown -= Time.deltaTime;
		foreach (KeyCode key in inputKeys){
			if (Input.GetKeyDown(key)){
				switch (key) 
				{
				case KeyCode.Alpha1:
					//shoot shrinker ray
					currentWeaponName = "Shrinker Ray";
					Debug.Log(currentWeaponName);
					break;
				case KeyCode.Alpha2:
					//shoot swap mind ray
					currentWeaponName = "Swap Mind Ray";
					Debug.Log(currentWeaponName);
					break;
				default:
					break;
				}
			}
		}


		switch (currentWeaponName) {
		case "Shrinker Ray":
			if (Input.GetButton("Fire1")){
				//Shoot shrinkerRay
				ShootShrinkerRay();
			}
			break;
		case "Swap Mind Ray":
			//Debug.Log ("I havbe swap mind ray now");
			break;
		default:
			//Debug.Log ("I have nothing!!!");
			break;
		}

	}

	void ShootShrinkerRay(){
		if (cooldown > 0){
			return;
		}
		Debug.Log ("shooting shrinker ray!!");
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
				rr.GetComponent<PhotonView> ().RPC ("TakeHit", PhotonTargets.All, 120, "Shrinker Ray");
			}
		}

		//if (Physics.Raycast(ray, out hitInfo)) {
		//	Debug.Log ("I hit:" + hitInfo.collider.name);
		//}
		cooldown = shrinkerRayFireRate;
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
