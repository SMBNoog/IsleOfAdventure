using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BossSingleton : Enemy, IAttacker
{

    public Slider HP_Slider;

    private Animator animator; 
    public static BossSingleton instance;

    public GameObject attackingsprite;
    public GameObject fireball;
    public Transform transformf;

    // Interface properties
    public WellBeingState wellBeing { get; set; }
    public ActionState actionState { get; set; }
    public TypeOfStatIncrease typeOfStatIncrease { get; set; }
    public Team Team { get { return Team.Enemy; } }
    public Vector2 Pos { get { return transform.position; } }
    public float Atk { get; set; }
    
    void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        HP = 10000000f;
        Atk = 0;
        Def = 0.25f;
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        HP_Slider.maxValue = HP;
        
    }

    void Update()
    {
        if (HP_Slider.value != HP)
            HP_Slider.value = HP;
    }

        void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            animator.SetBool("In Fireball Area", false);
            attackingsprite.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            animator.SetBool("In Fireball Area", true);
        }
    }

    public override void Die()
    {        
        Destroy(transform.parent.gameObject, 1f); //wait until respawn, disable
    }

}