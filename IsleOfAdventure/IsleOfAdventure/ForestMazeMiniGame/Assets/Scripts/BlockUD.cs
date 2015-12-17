using UnityEngine;
using System.Collections;

public class BlockUD : MonoBehaviour
{
    bool movingUp = true;
    public float targetSpeed = 1f;
    public Rigidbody2D rigidbody2D;
    public float wallTestDistance = 1.5f;
    public Transform centerPoint;
    private Vector2 velocity;

    void Start()
    {
        velocity = rigidbody2D.velocity;
        StartCoroutine(DelayMovement());
    }

    IEnumerator DelayMovement()
    {
        while (true)
        {
            float DelayTime = Random.Range(3f, 5f);
            yield return new WaitForSeconds(DelayTime);
            DelayTime = 2;
            while (DelayTime > 0)
            {
                DelayTime -= Time.fixedDeltaTime;
                if (movingUp && velocity.y < targetSpeed)
                    rigidbody2D.MovePosition(transform.position + Vector3.up * Time.deltaTime);
                else if (!movingUp && velocity.y > -targetSpeed)
                    rigidbody2D.MovePosition(transform.position + -Vector3.up * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }
            rigidbody2D.velocity = new Vector2(0f, 0f);
        }
    }

    void FixedUpdate()
    {
        int layerMask;
        layerMask = int.MaxValue;
        layerMask = layerMask ^ 1 << LayerMask.NameToLayer("Block");

        if (movingUp)
        {
            RaycastHit2D hit = Physics2D.Raycast(centerPoint.position, Vector2.up, wallTestDistance, layerMask);
            if (hit.collider != null && hit.collider.gameObject.tag != "Player")
                movingUp = false;
            updateDirection();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(centerPoint.position, -Vector2.up, wallTestDistance, layerMask);
            if (hit.collider != null && hit.collider.gameObject.tag != "Player")
            {
                movingUp = true;
                updateDirection();
            }
        }
    }

    private void updateDirection()
    {
        Vector3 scale = transform.localScale;
        scale.y = Mathf.Abs(scale.y) * (movingUp ? 1f : -1f);
        transform.localScale = scale;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(centerPoint.position, centerPoint.position + (movingUp ? Vector3.up : -Vector3.up) * wallTestDistance);
    }
}