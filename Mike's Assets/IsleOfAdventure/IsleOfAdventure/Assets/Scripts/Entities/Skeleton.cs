using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Skeleton : Enemy, IAttacker
{
    public static int numberOfTutorialSkeletons;
    public Slider HP_Slider;

    public ISpawner Spawner  // reference to the interface to Respawn this enemy when it dies
    {
        get; set;
    }

    // Interface properties
    public WellBeingState wellBeing { get; set; }    
    public ActionState actionState { get; set; }
    public TypeOfStatIncrease typeOfStatIncrease { get; set; }
    public Team Team { get { return Team.Enemy; } }
    public Vector2 Pos { get { return transform.position; } }
    public float Atk { get; set; }
    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();  
        HP_Slider.maxValue = HP;

        // check if in castle
        //if(GameInfo.AreaToTeleportTo == GameInfo.Area.Castle)
        //    CastleController.RoomOneEnemies += 1;
    }

    // Skeleton Instance
    public void Initialize(float HP, float Atk, float Def, float amountToStatToGive, TypeOfStatIncrease stat)
    {
        float multiplier = UnityEngine.Random.Range(-.1f, .3f);
        this.HP = HP + (HP * multiplier);
        this.Atk = Atk + (Atk * multiplier);
        this.Def = Def + (Def * multiplier);

        if(GameInfo.AreaToTeleportTo == GameInfo.Area.TutorialArea)
            numberOfTutorialSkeletons += 1;

        amountOfStatToGiveAponDeath = amountToStatToGive;
        typeOfStatIncrease = stat;
        
    }

    IEnumerator SwitchToPatrol()
    {
        switching = true;
        yield return new WaitForSeconds(UnityEngine.Random.Range(3, 5));
        if (anim.GetBool("Idle"))
        {
            anim.SetBool("Patrol", true);
            anim.SetBool("Idle", false);
        }
        switching = false;
    }

    IEnumerator SwitchToIdle()
    {
        switching = true;
        yield return new WaitForSeconds(UnityEngine.Random.Range(3, 5));
        if (anim.GetBool("Patrol"))
        {
            anim.SetBool("Patrol", false);
            anim.SetBool("Idle", true);
        }
        switching = false;
    }

    void Update()
    {
        if (HP_Slider.value != HP)
            HP_Slider.value = HP;

        if (anim.GetBool("Idle") && !switching)
            StartCoroutine(SwitchToPatrol());
        else if (anim.GetBool("Patrol") && !switching)
            StartCoroutine(SwitchToIdle());
    }   

    public override void Die()
    {
        if(GameInfo.AreaToTeleportTo == GameInfo.Area.TutorialArea)
            numberOfTutorialSkeletons -= 1;

        anim.SetBool("Death", true);
        anim.SetTrigger("DeathAni");
        //Debug.Log("I am of type : " + typeOfStatIncrease);
        GiveStatsToPlayerAponDeath(amountOfStatToGiveAponDeath, typeOfStatIncrease);
        rb2D.isKinematic = true;
        ////rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        foreach(Collider2D col in GetComponents<Collider2D>())
        {
            col.enabled = false;
        }

        Spawner.Died(this);

        //CastleController.RoomOneEnemies -= 1;

        Destroy(gameObject, 3f); //wait until respawn, disable
    }    
    
}