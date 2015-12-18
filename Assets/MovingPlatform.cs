using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {     
    
    public float speed;
    public float length;
​
    public bool reverseDirection = false;
​
    public bool lastPlatform = false;
​
    private Transform myTransform;
    private Vector3 origin;
​
    void Start()
    {
        myTransform = transform;
        origin = transform.position;
    }
​
    void Update()
    {
        if (!reverseDirection)
            myTransform.position = new Vector3(origin.x + Mathf.PingPong(Time.time * speed, length), myTransform.position.y, myTransform.position.z);
        else
            myTransform.position = new Vector3(origin.x + Mathf.PingPong(Time.time * speed, length), myTransform.position.y, myTransform.position.z);
    }
​
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        if (other.gameObject.transform.parent != null)
    //            other.gameObject.transform.parent = null;

    //        other.gameObject.transform.parent = this.transform;
    //    }
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") //avoid weapon
            other.gameObject.transform.SetParent(transform);
    }
}
