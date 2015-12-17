using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public GameObject fireball;

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = -transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Player")
        {
            //deal x amount of damage
            Destroy(fireball);
        }
        else
        {
            Destroy(fireball);
        }
    }
}