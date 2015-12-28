using UnityEngine;
using System.Collections;
using System;

public class Fireball : MonoBehaviour, IAttacker
{
    public float speed = 10f;
    public GameObject fireball;

    public WellBeingState wellBeing
    {
        get
        {
            return wellBeing;
        }

        set
        {
            wellBeing = value;
        }
    }

    public ActionState actionState
    {
        get
        {
            return actionState;
        }

        set
        {
            actionState = value;
        }
    }

    public TypeOfStatIncrease typeOfStatIncrease
    {
        get
        {
            return TypeOfStatIncrease.HP;
        }

        set
        {
            typeOfStatIncrease = value;
        }
    }

    public Team Team
    {
        get
        {
            return Team.Enemy;
        }
    }

    public Vector2 Pos
    {
        get
        {
            return transform.position;
        }
    }

    public float Atk
    {
        get
        {
            return 2000f;
        }

        set
        {
            Atk = value;
        }
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = -transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject, 0.1f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "FireBallDestroyer")
            Destroy(gameObject);
    }
}