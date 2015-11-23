using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour
{
    public GameObject openchest;
    public GameObject UI;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            openchest.SetActive(true);
            Debug2Screen.Log("You Win");
            GiveWeapon();
        }
    }

    void GiveWeapon()
    {
        if (Random.Range(0f, 1f) < .70f)
            Debug2Screen.Log("You Obtained The Bronze Sword");
        else if (Random.Range(0f, 1f) < .90f)
            Debug2Screen.Log("You Obtained The Silver Sword");
        else if (Random.Range(0f, 1f) < .98f)
            Debug2Screen.Log("You Obtained The Gold Sword");
        else
            Debug2Screen.Log("You Obtained The Epic Sword");
    }
}