using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour {
	private bool Opened = false;
	private bool InZone = false; 
	public string PlayerHeadTag = "MainCamera"; //player's head with collider in trigger mode. Type your tag here (usually it is MainCamera)
	public float OpeningSpeed = 1;
	public float ClosingSpeed = 1.3f;
	public bool OpenByButton = true; //decides, how door opens - automatic or by pressing the button on the keyboard
	public KeyCode OpenButton = KeyCode.E; //Button on the keyboard to open the door
	public bool AutoClose = true; //close the door when you go away from door
	public GameObject SoundFX; //GameObject with audiosource;
	public AudioClip OpenFX; //sound when door opening
	public AudioClip CloseFX; //sound when door closing
	public AudioClip ClosedFX; //sound when door is closed and you have no key
	public bool ShowText = true; //show text tips when you stand close to door
	public GameObject TextPrefab; 
	private GameObject TextObj;
	public string DoorText = "Press \"E\" to open";
	public bool OpenWithKey = false; //open the door with key (add the key prefab on the scene and put desired door into the "Door" slot in KeyScript inspector)
	public bool isKey = false;
	public GameObject LockPrefab; //if you have an outside lock, you can place the prefab here. The door will automatically become "OpenWithKey"
	private Animation LockAnim;

	void Start () {
		if (OpenByButton) {
			DoorText = "Press \"" + OpenButton + "\"" + " to open";
			TextObj = Instantiate (TextPrefab, Vector3.zero, new Quaternion (0, 0, 0, 0)) as GameObject;
			if(!OpenWithKey && LockPrefab == null){
				TextObj.GetComponentInChildren<Text> ().text = DoorText;
				TextObj.GetComponent<Canvas>().enabled = false;
			}
		}
		if (LockPrefab != null) {
			LockAnim = LockPrefab.GetComponent<Animation>();
			OpenWithKey = true;
		}
		if (OpenWithKey && !isKey) {
			DoorText = "You need a key";
			TextObj.GetComponentInChildren<Text> ().text = DoorText;
			TextObj.GetComponent<Canvas>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(InZone && Input.GetKeyDown(OpenButton) && OpenByButton && !Opened && OpenWithKey && isKey){
			if(LockPrefab != null){
				LockAnim.Play("Lock_open");
				Invoke("DoorOpening", 1);
			} 
			else{
				DoorOpening();
			}
		}
		if (InZone && Input.GetKeyDown (OpenButton) && OpenByButton && !Opened && OpenWithKey && !isKey && ClosedFX != null) {
			SoundFX.GetComponent<AudioSource>().clip = ClosedFX;
			SoundFX.GetComponent<AudioSource>().Play();
		}
		if(InZone && Input.GetKeyDown(OpenButton) && OpenByButton && !Opened && !OpenWithKey){
			DoorOpening();
		}

	}

	void OnTriggerEnter(Collider other){
		if (other.GetComponent<Collider> ().tag == PlayerHeadTag && !Opened) {
			InZone = true;
			if(OpenByButton){
				if(OpenWithKey && isKey){
					DoorText = "Press \"" + OpenButton + "\"" + " to open";
					TextObj.GetComponentInChildren<Text> ().text = DoorText;
				}
				TextObj.GetComponent<Canvas>().enabled = true;

			}
			if(!OpenByButton){
				DoorOpening();
			}
		}
	}
	void OnTriggerExit(Collider other){
		if (other.GetComponent<Collider> ().tag == PlayerHeadTag) {
			InZone = false;
			if(OpenByButton){
				TextObj.GetComponent<Canvas>().enabled = false;
			}
			if(Opened && AutoClose){
				DoorClosng();
			}
		}
	}
	public void OpenText(){
		DoorText = "Press \"" + OpenButton + "\"" + " to open";
		TextObj.GetComponentInChildren<Text> ().text = DoorText;
	}
	void DoorOpening(){
		GetComponent<Animation>().Play();
		GetComponent<Animation>()["Door_open"].speed = OpeningSpeed;
		GetComponent<Animation> () ["Door_open"].normalizedTime = GetComponent<Animation> () ["Door_open"].normalizedTime;
		if(OpenFX != null){
			SoundFX.GetComponent<AudioSource> ().pitch = Random.Range (0.8f, 1.2f);
			SoundFX.GetComponent<AudioSource>().clip = OpenFX;
			SoundFX.GetComponent<AudioSource>().Play();
		}
		Opened = true;
		if (OpenByButton) {
			TextObj.GetComponent<Canvas>().enabled = false;
		}
		OpenWithKey = false;
	}

	void DoorClosng(){
		if (GetComponent<Animation> () ["Door_open"].normalizedTime < 0.98f && GetComponent<Animation> () ["Door_open"].normalizedTime > 0) {
			GetComponent<Animation> () ["Door_open"].speed = -ClosingSpeed;
			GetComponent<Animation> () ["Door_open"].normalizedTime = GetComponent<Animation> () ["Door_open"].normalizedTime;
			GetComponent<Animation> ().Play ();
		} 
		else {
			GetComponent<Animation> () ["Door_open"].speed = -ClosingSpeed;
			GetComponent<Animation> () ["Door_open"].normalizedTime = 0.6f;
			GetComponent<Animation> ().Play ();
		}
		Opened = false;
	}
	void CloseSound(){
		if(GetComponent<Animation> () ["Door_open"].speed <0 && CloseFX != null){
			SoundFX.GetComponent<AudioSource> ().pitch = Random.Range (0.8f, 1.2f);
			SoundFX.GetComponent<AudioSource>().clip = CloseFX;
			SoundFX.GetComponent<AudioSource>().Play();
		}
	}
}
