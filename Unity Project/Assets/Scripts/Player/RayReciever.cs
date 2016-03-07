using UnityEngine;
using System.Collections;

public class RayReciever : MonoBehaviour {
	int resistancePoints = 100;
	public int currentResistance = 100;
	public bool canGetShotShrinker = true;
	public float shrinkedTime = 6f;
	public float shrinkedCooldownTime = 25f;
	public Vector3 shrinkedSize = new Vector3 (0.1f, 0.1f, 0.1f);
	public Vector3 originalSize = new Vector3 (1f, 1f, 1f);

	void Start () {
		currentResistance = resistancePoints;
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
		default:
			Destroy (gameObject);
			break;
		}
	}
}
