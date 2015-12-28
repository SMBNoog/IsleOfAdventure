using UnityEngine;
using System.Collections;

public class BlockTrigger : MonoBehaviour {

    public Transform triggerSpot;

    public GameObject block1;
    public GameObject block2;
    public GameObject block3;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.transform.position.y > triggerSpot.position.y)
        {
            block1.gameObject.SetActive(true);
            block2.gameObject.SetActive(true);
            block3.gameObject.SetActive(true);
        }
    }
}
