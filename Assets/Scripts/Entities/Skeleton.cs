using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Skeleton : Enemy, IAttacker
{
    public static int numberOfSkeletons;
    public Slider HP_Slider;

    // Interface properties
    public WellBeingState wellBeing { get; set; }    
    public ActionState actionState { get; set; }
    public TypeOfStatIncrease typeOfStatIncrease { get; set; }
    public Team Team { get { return Team.Enemy; } }
    public Vector2 Pos { get { return transform.position; } }
    public float Atk { get; set; }
    public DeadEnemy startingSkeleton;

    //private float distaneOfRay = 10;
    //private List<RaycastHit2D> rays;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        myT = GetComponent<Transform>();
        anim = GetComponent<Animator>();  
        HP_Slider.maxValue = HP;

        spawnPosition = myT.position;
        //Debug.Log(this + " HP: " + HP +      " | ATK: " + Atk + " | DEF: " + Def + " | Speed: " + Speed);
    }

    // Skeleton Instance
    public void Initialize(float HP, float Atk, float Def, float Speed, float amountToStatToGive, TypeOfStatIncrease stat)
    {
        float multiplier = UnityEngine.Random.Range(-.1f, .1f);
        this.HP = HP + (HP * multiplier);
        this.Atk = Atk + (Atk * multiplier);
        this.Def = Def + (Def * multiplier);
        this.Speed = Speed;
        numberOfSkeletons += 1;
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


        //rays = new List<RaycastHit2D>();
        //RayCasting2D();
        //distanceFromPlayer = Vector2.Distance(Pos, targetPlayer);

        //////RaycastHit2D rayToPlayer = Physics2D.Linecast(transform.position, targetPlayer, 1 << 10);

        //////if (myT.position != lastPosition)
        //////{
        //////    direction = myT.position - lastPosition;
        //////    //direction.Normalize();
        //////    anim.SetFloat("DirectionX", direction.x);
        //////    anim.SetFloat("DirectionY", direction.y);
        //////}

        //////lastPosition = myT.position;
    }



    //void RayCasting2D()
    //{
    //    float m = 0f;
    //    switch (enemyFacing)
    //    {
    //        case EnemyFacing.down: m = Mathf.PI; break;
    //        case EnemyFacing.left: m = Mathf.PI / 2; break;
    //        case EnemyFacing.right: m = Mathf.PI / -2; break;
    //        case EnemyFacing.up: m = 0f; break;
    //    }

    //    Debug.Log("Enemy Facing "+ enemyFacing + ". M = " + m);

    //    for (int i = 0; i < 5; i++)
    //    {
    //        RaycastHit2D ray = Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos(m), Mathf.Sin(m)),
    //            i == 0 || i == 4 ? distaneOfRay / 2 : distaneOfRay, 1 << 10);
    //        rays.Add(ray);
    //        m += Mathf.PI / 4;
    //    }

    //    if(rays.Count > 0)
    //    {
    //        foreach (RaycastHit2D ray in rays)
    //        {
    //            //Debug.Log(ray.collider);
    //            //if(ray.collider != null/* && ray.collider.gameObject.tag == "Player"*/)
    //            //{
    //            //    Debug.Log(ray.collider.name + "<<< Hit the Ray!");
    //            //    targetPlayer = ray.collider.gameObject;
    //            //    actionState = ActionState.ChargeAtPlayer;
    //            //}
    //            if(ray.collider != null)
    //            {
    //                IAttacker attacker = Interface.Find<IAttacker>(ray.collider.gameObject);
    //                if (attacker != null && attacker.Team == Team.Player)
    //                {
    //                    Debug.Log(ray.collider.name + "<<< Hit the Ray!");
    //                    targetPlayer = attacker.Pos;
    //                    actionState = ActionState.ChargeAtPlayer;
    //                }
    //                else
    //                    actionState = ActionState.Idle;
    //            }
    //        }
    //    }
    //}

    void FixedUpdate()
    {
        
    }

    public override void Die()
    {
        // Play death animation
        numberOfSkeletons -= 1;
        anim.SetTrigger("Death");
        Debug.Log("I am of type : " + typeOfStatIncrease);
        GiveStatsToPlayerAponDeath(amountOfStatToGiveAponDeath, typeOfStatIncrease);
        rb2D.isKinematic = true;
        foreach(Collider2D col in GetComponents<Collider2D>())
        {
            col.enabled = false;
        }
        // assign method from another class to it
        //SpawnEnemies.enemiesInstantiated.RemoveAt(SpawnEnemies.enemiesInstantiated.Count - 1);
        Destroy(gameObject, 3f);
    }
    

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = new Color(1f, .0f, 0f, .2f);
    //    Gizmos.DrawSphere(transform.position, distaneOfRay);
    //    Gizmos.color = new Color(1f, .0f, 0f, .8f);
    //    float m = Mathf.PI / -2;
    //    for (int i = 0; i < 5; i++)
    //    {
    //        Gizmos.DrawRay(transform.position, new Vector2(Mathf.Cos(m), Mathf.Sin(m)));
    //        m += Mathf.PI / 4;
    //    }
    //}
}