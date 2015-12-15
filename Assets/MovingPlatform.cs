using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    iTweenPath myPath;
    
    
    void Start()
    {
        myPath = GetComponent<iTweenPath>();
        myPath.nodes[0] = transform.position;
    } 
       	
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") //avoid weapon
            other.gameObject.transform.SetParent(transform);
    }    
}
