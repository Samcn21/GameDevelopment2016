using UnityEngine;
using System.Collections;
using System.Runtime.ConstrainedExecution;

public class DemolitionRepair : MonoBehaviour {
	
	public float demolitionTime = 3f;
	public float repairTime = 1.5f;

	public bool isRepaired = false;
	public bool isDestroyed = false;
	public bool oneTimeCall = false;
    public bool insideRepair = false;

	public MachineControl mc;
	public string characterTag = "";

    [SerializeField] private float maxRepairRange = 3;

	void Start () {
		characterTag = transform.tag.ToString ();
	}

	void OnTriggerStay(Collider other) {
	    insideRepair = true;
		//if (other.name == "Machine") {
			if (oneTimeCall) {
				mc = other.GetComponent<MachineControl> ();
				mc.GetComponent<PhotonView> ().RPC ("DemolitionRepair", PhotonTargets.All, characterTag, isRepaired, isDestroyed);
				oneTimeCall = false;
                
				//Debug.Log ("Invoked Demolition Repair function in Machine Control script");				
			}
		//}
	}

    void OnTriggerExit(Collider other) {
        insideRepair = false;
    }


	void Update () {
		//we need to disable the player's control of movement as soon as player hold E to fix or destroy
		//not to let the player move during repairing or destroying
		if (Input.GetKey(KeyCode.E) && insideRepair){
            Ray ray = new Ray(transform.position, GetComponentInChildren<Camera>().transform.forward);
		    RaycastHit hitInfo;
		    if (Physics.Raycast(ray, out hitInfo, maxRepairRange))
		    {
		        MachineControl mc = hitInfo.transform.GetComponent<MachineControl>();
		        if (mc == null)
		        {
		            repairTime = 1.5f;
		            demolitionTime = 3;
		            return;
		        }

                if (characterTag == "MadScientist") {
                    repairTime -= Time.deltaTime;
                    if (repairTime < 0) {
                        //animation of fixing
                        isRepaired = true;
                        oneTimeCall = true;
                        repairTime = 1.5f;
                    }
                }
                else if (characterTag == "Nephew") {
                    demolitionTime -= Time.deltaTime;
                    if (demolitionTime < 0) {
                        //animation of destroying
                        isDestroyed = true;
                        oneTimeCall = true;
                        demolitionTime = 3f;
                    }
                }

            }
			
		}
	}
}
