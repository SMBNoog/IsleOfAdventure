using UnityEngine;
using System.Collections;

public class LockScript : MonoBehaviour {
	public GameObject LockObj;
	private Animation LockBreak;
	public GameObject Door;
	public string WeaponTag = "Weapon";
	public int LockLife = 1;
	public GameObject FX;
	// Use this for initialization
	void Start () {
		LockBreak = GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter(Collision other){
		if (other.transform.gameObject.tag == WeaponTag && LockLife > 0) {
			LockHit();
		}
	}
	public void LockHit(){
		LockLife--;
		if(LockLife <= 0){
			LockBreaking();
		}
	}
	void EnablePhysics(){
		LockObj.GetComponent<Rigidbody> ().useGravity = true;
		LockObj.GetComponent<Rigidbody> ().isKinematic = false;
	}
	void LockBreaking(){
		DoorScript doorScript = Door.GetComponent<DoorScript> ();
		doorScript.OpenWithKey = false;
		doorScript.OpenText ();
		LockBreak.Play ("Lock_breaking");
		FX.SetActive (true);
	}
}
