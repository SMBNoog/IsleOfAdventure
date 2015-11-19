using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Solider : Enemy, IAttacker
{
    public static int numberOfSoliders;
    public Slider HP_Slider;

    // Interface properties
    public WellBeingState wellBeing { get; set; }
    public ActionState actionState { get; set; }
    public TypeOfStatIncrease typeOfStatIncrease { get; set; }
    public Team Team { get { return Team.Enemy; } }
    public Vector2 Pos { get { return transform.position; } }
    public float Atk { get; set; }

    //private float distaneOfRay = 10;
    //private List<RaycastHit2D> rays;
    private Vector3 direction;
    private Vector3 lastPosition;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        myT = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        HP_Slider.maxValue = HP;

        Debug.Log(this + " HP: " + HP + " | ATK: " + Atk + " | DEF: " + Def + " | Speed: " + Speed);
    }

    // Skeleton Instance
    public void Initialize(float HP, float Atk, float Def, float Speed, float amountToStatToGive, TypeOfStatIncrease stat)
    {
        float multiplier = UnityEngine.Random.Range(-.1f, .1f);
        this.HP = HP + (HP * multiplier);
        this.Atk = Atk + (Atk * multiplier);
        this.Def = Def + (Def * multiplier);
        this.Speed = Speed;
        numberOfSoliders += 1;
        amountOfStatToGiveAponDeath = amountToStatToGive;
        typeOfStatIncrease = stat;
    }

    void Update()
    {
        if (HP_Slider.value != HP)
            HP_Slider.value = HP;

        //rays = new List<RaycastHit2D>();
        //RayCasting2D();
        //distanceFromPlayer = Vector2.Distance(Pos, targetPlayer);

        if (myT.position != lastPosition)
        {
            direction = myT.position - lastPosition;
            direction.Normalize();
            anim.SetFloat("DirectionX", direction.x);
            anim.SetFloat("DirectionY", direction.y);
        }

        lastPosition = myT.position;
    }

    void FixedUpdate()
    {

    }

    public override void Die()
    {
        // Play death animation
        numberOfSoliders -= 1;
        // Increase HP, Atk or Def for the Player
        Debug.Log("I am of type : " + typeOfStatIncrease);
        GiveStatsToPlayerAponDeath(amountOfStatToGiveAponDeath, typeOfStatIncrease);
        Destroy(gameObject);

    }
}