using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = -transform.up * speed;
    }
}