using UnityEngine;
using System.Collections;

public class BossSingleton : MonoBehaviour
{
    private Animator animator; 
    public static BossSingleton instance;

    public GameObject attackingsprite;
    public GameObject fireball;
    public Transform transformf;
    public float health = 500000f;

    private BossSingleton()
    { }

    void OnTriggerStay2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "Player")
        {
            animator.SetBool("In Fireball Area", true);
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "Player")
        {
            animator.SetBool("In Fireball Area", false);
            attackingsprite.SetActive(false);
        }
    }

    void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        //check for weapon
        //deal damage to boss based on weapon type
    }
}