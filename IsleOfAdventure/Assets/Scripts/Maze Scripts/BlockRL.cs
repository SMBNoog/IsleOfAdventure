using UnityEngine;
using System.Collections;

public class BlockRL : MonoBehaviour
{
    bool movingRight = true;
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
        yield return new WaitForSeconds(Random.Range(1f,2f));
        while (true)
        {
            float DelayTime = Random.Range(4f, 8f);
            yield return new WaitForSeconds(DelayTime);
            DelayTime = Random.Range(1f, 3f);

            while (DelayTime > 0)
            {
                DelayTime -= Time.fixedDeltaTime;
                if (movingRight && velocity.x < targetSpeed)
                    myrigidbody2D.MovePosition(transform.position + Vector3.right * Time.fixedDeltaTime);
                else if (!movingRight && velocity.x > -targetSpeed)
                    myrigidbody2D.MovePosition(transform.position + -Vector3.right * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }
            myrigidbody2D.velocity = new Vector2(0f, 0f); 
        }
    }

    void FixedUpdate()
    {
        if (movingRight)
        {
            RaycastHit2D hit = Physics2D.Raycast(centerPoint.position, Vector2.right, wallTestDistance);
            if (hit.collider != null && hit.collider.gameObject.tag != "Player")
            {
                movingRight = false;
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(centerPoint2.position, -Vector2.right, wallTestDistance);
            if (hit.collider != null && hit.collider.gameObject.tag != "Player")
            {
                movingRight = true;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(centerPoint.position, centerPoint.position + (movingRight ? Vector3.right : -Vector3.right) * wallTestDistance);
        Gizmos.DrawLine(centerPoint2.position, centerPoint2.position + (movingRight ? Vector3.right : -Vector3.right) * wallTestDistance);
    }
}