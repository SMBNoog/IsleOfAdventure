﻿using UnityEngine;
using System.Collections;

public class BlockUD : MonoBehaviour
{
    bool movingUp = true;
    public float targetSpeed = 1f;
    public Rigidbody2D myrigidbody2D;
    public float wallTestDistance = 1.5f;
    public Transform centerPoint;
    public Transform centerPoint2;
    private Vector2 velocity;

    void Start()
    {
        velocity = myrigidbody2D.velocity;
        StartCoroutine(DelayMovement());
    }

    IEnumerator DelayMovement()
    {
        while (true)
        {
            float DelayTime = Random.Range(3f, 5f);
            yield return new WaitForSeconds(DelayTime);
            DelayTime = Random.Range(1f, 3f);
            while (DelayTime > 0)
            {
                DelayTime -= Time.fixedDeltaTime;
                if (movingUp && velocity.y < targetSpeed)
                    myrigidbody2D.MovePosition(transform.position + Vector3.up * Time.deltaTime);
                else if (!movingUp && velocity.y > -targetSpeed)
                    myrigidbody2D.MovePosition(transform.position + -Vector3.up * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }
            myrigidbody2D.velocity = new Vector2(0f, 0f);
        }
    }

    void FixedUpdate()
    {
        if (movingUp)
        {
            RaycastHit2D hit = Physics2D.Raycast(centerPoint.position, Vector2.up, wallTestDistance);
            if (hit.collider != null && hit.collider.gameObject.tag != "Player")
                movingUp = false;
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(centerPoint2.position, -Vector2.up, wallTestDistance);
            if (hit.collider != null && hit.collider.gameObject.tag != "Player")
            {
                movingUp = true;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(centerPoint.position, centerPoint.position + (movingUp ? Vector3.up : -Vector3.up) * wallTestDistance);
        Gizmos.DrawLine(centerPoint2.position, centerPoint2.position + (movingUp ? Vector3.up : -Vector3.up) * wallTestDistance);
    }
}