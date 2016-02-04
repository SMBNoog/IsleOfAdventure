using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smooth;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, smooth);
    }
}