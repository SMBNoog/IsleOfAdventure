using UnityEngine;
using System.Collections;

public class Leaves : MonoBehaviour {

	void OnEnable()
    {
        Destroy(gameObject, 2f);
    }
}
