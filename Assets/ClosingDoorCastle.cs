using UnityEngine;
using System.Collections;

public class ClosingDoorCastle : MonoBehaviour {

    public GameObject closingDoor;

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.transform.position.y > transform.position.y)
        {
            closingDoor.gameObject.SetActive(true);
        }
    }
}
