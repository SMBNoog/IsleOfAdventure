using UnityEngine;
using System.Collections;

public class LightRot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (Vector3.right*1f);
	}
}
