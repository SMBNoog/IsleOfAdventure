using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialIntroRules : MonoBehaviour {

    public Image howToBoostStats_Panel;

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            howToBoostStats_Panel.gameObject.SetActive(true);
        }
    }


}
