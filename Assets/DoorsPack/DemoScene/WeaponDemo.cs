using UnityEngine;
using System.Collections;

public class WeaponDemo : MonoBehaviour {
	private RaycastHit hit;
	public GameObject sight;
	//public LayerMask layerMask;
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1) && sight != null) {
			if(!sight.activeInHierarchy){
				sight.SetActive(true);
			}
			else{
				sight.SetActive(false);
			}
		}
	if(Input.GetMouseButtonDown(0)){
			if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 20, out hit)){
				if(hit.transform.gameObject.tag == "Lock"){
					LockScript lockScript = hit.transform.gameObject.GetComponent<LockScript>();
					lockScript.LockHit();
				}

			}
		}
	}
	void FixedUpdate () {

	}
}
