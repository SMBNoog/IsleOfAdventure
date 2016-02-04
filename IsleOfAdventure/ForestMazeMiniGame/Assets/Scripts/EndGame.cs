using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("You Win");
            GiveWeapon();
        }
    }

    void GiveWeapon()
    {
        if (Random.Range(0f, 1f) < .60f)
            Debug.Log("You Obtained The Common Sword");
        else if (Random.Range(0f, 1f) < .90f)
            Debug.Log("You Obtained The Uncommon Sword");
        else
            Debug.Log("You Obtained The Rare Sword");
    }
}