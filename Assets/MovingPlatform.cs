using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
        	
    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.SetParent(transform);
    }    
}
