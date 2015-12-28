using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour
{
    public float speed;

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
        GetComponent<Rigidbody2D>().velocity = movement * speed;
    }
}