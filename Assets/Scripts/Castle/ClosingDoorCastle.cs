using UnityEngine;
using System.Collections;

public class ClosingDoorCastle : MonoBehaviour {

    public GameObject closingDoorL;
    public GameObject closingDoorR;

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && other.gameObject.transform.position.y > transform.position.y)
        {
            closingDoorL.gameObject.SetActive(true);
            closingDoorR.gameObject.SetActive(true);
        }
    }
}
