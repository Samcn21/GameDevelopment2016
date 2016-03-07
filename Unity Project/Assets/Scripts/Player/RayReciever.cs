using UnityEngine;
using System.Collections;

public class RayReciever : MonoBehaviour {
	int resistancePoints = 100;
	public int currentResistance = 100;
	public bool canGetShotShrinker = true;
	public bool canGetShotForceDome = true;
	public bool canGetShotTeleporterExile = true;
	public bool canGetShotFreezeRay = true;



	public float shrinkedTime = 10f;
	public float shrinkedCooldownTime = 25f;

	public float forceDomeTime = 10f;
	public float forceDomeCooldownTime = 20f;
	public Vector3 forceDomePosition = new Vector3(6f, -1f, 50f);

	public float teleporterExileTime = 15f;
	public float teleporterExileCooldownTime = 45f;
	public Vector3 teleporterExilePosition = new Vector3(6f, -1f, 50f);

	public float freezeTime = 5f;
	public float freezeCooldownTime = 20f;

	public Vector3 shrinkedSize = new Vector3 (0.1f, 0.1f, 0.1f);
	public Vector3 originalSize = new Vector3 (1f, 1f, 1f);

	void Start () {
		currentResistance = resistancePoints;
		forceDomePosition = new Vector3(6f, -1f, 50f);
		//GameObject thisReciever = GameObject.Find (this.name);
		//Debug.Log(((MonoBehaviour)thisReciever.GetComponent("FirstPersonController")).WalkSpeed = 1f);
	}

	[PunRPC]
	public void TakeHit (int hitPoints, string weaponType){
		StartCoroutine(DoAction (hitPoints, weaponType));
	}

	//we need to check the type of weapon that has been shot by Mad Scientist
	//Depends on his weapon type, different actions woulad happends

	IEnumerator DoAction(int hitPoints, string wType) {
		//Destroy Object just for test for now!!!
		switch (wType)
		{
		case "Shrinker Ray":
			//Debug.Log ("Shrinker Ray Affected!!!");
			currentResistance -= hitPoints;
			if ((currentResistance < 0) && (canGetShotShrinker)) {
				transform.localScale = shrinkedSize;
				canGetShotShrinker = false;
				yield return new WaitForSeconds (shrinkedTime);
				transform.localScale = originalSize;
				yield return new WaitForSeconds (shrinkedCooldownTime);
				currentResistance = resistancePoints;
				canGetShotShrinker = true;
			}
			break;
		case "Force Dome":
			//need to work on this more, now it just move forcedome.
			currentResistance -= hitPoints;
			if ((currentResistance < 0) && (canGetShotForceDome)) {
				
				GameObject forceDome = GameObject.FindGameObjectWithTag("ForceDome");

				forceDome.transform.position = this.transform.position;
				canGetShotForceDome = false;

				yield return new WaitForSeconds (forceDomeTime);
				forceDome.transform.position = forceDomePosition;

				yield return new WaitForSeconds (forceDomeCooldownTime);

				currentResistance = resistancePoints;
				canGetShotForceDome = true;
			}
			break;

		case "Teleporter Exile":
			currentResistance -= hitPoints;
			if ((currentResistance < 0) && (canGetShotTeleporterExile)) {

				GameObject teleporterSpawn = GameObject.FindGameObjectWithTag("TeleporterExileSpawn");
				Vector3 beforeTeleportPosition = this.transform.position;

				this.transform.position = teleporterSpawn.transform.position;
				canGetShotTeleporterExile = false;

				//Debug.Log (forceDomePosition);

				yield return new WaitForSeconds (teleporterExileTime);
				this.transform.position = beforeTeleportPosition;

				yield return new WaitForSeconds (teleporterExileCooldownTime);

				currentResistance = resistancePoints;
				canGetShotTeleporterExile = true;
			}
			break;
		case "Freeze Ray":
			currentResistance -= hitPoints;
			if ((currentResistance < 0) && (canGetShotFreezeRay)) {

				((MonoBehaviour)this.gameObject.GetComponent("FirstPersonController")).enabled = false;

				canGetShotFreezeRay = false;

				yield return new WaitForSeconds (freezeTime);
				((MonoBehaviour)this.gameObject.GetComponent("FirstPersonController")).enabled = true;

				yield return new WaitForSeconds (freezeCooldownTime);

				currentResistance = resistancePoints;
				canGetShotFreezeRay = true;
			}
			break;
		default:
			Destroy (gameObject);
			break;
		}
	}
}
