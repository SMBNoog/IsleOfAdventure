using UnityEngine;
using Pathfinding;
using System;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class NewEnemyAI : MonoBehaviour {

    // What to chase
    private Transform target;

    // How many times each second we will update our path
    public float updateRate = 2f;

    private Seeker seeker;
    private Rigidbody2D rb;
    private Transform myT;
    private Vector3 direction;
    private Animator anim;

    public Path path;

    // AI speed per second
    public float speed = 300f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    // Max distance from AI to Waypoint for it to continue to next waypoint
    public float nextWaypointDistance = 3;

    // The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    public bool townNPC = false;
    private GameObject clockTower;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        myT = GetComponent<Transform>();
        anim = GetComponent<Animator>();

        clockTower = FindObjectOfType<ClockTower>().gameObject;
        if (townNPC)
        {
            anim.SetBool("CanSeePlayer", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Patrol", false);
            target = clockTower.transform;
            seeker.StartPath(transform.position, target.position, OnPathComplete);
            StartCoroutine(UpdatePath());
        }

            if (target == null)
        {
            //Debug.LogError("No Player found");
            return;
        }       

        //StartCoroutine(UpdatePath());
    }

    void Update()
    {

        if(target != null)
        {
            RaycastHit2D rayToPlayer = Physics2D.Linecast(transform.position, target.position, 1 << LayerMask.NameToLayer("Player"));

            float dis = rayToPlayer.distance;

            anim.SetFloat("DistanceFromTarget", dis);
            
            if(dis < 1)
            {
                target = null;
                return;
            }
            
            if (dis > 30f)
            {
                anim.SetBool("CanSeePlayer", false);
                anim.SetBool("Idle", true);
                Debug.Log("Can't see player");

                target = null;
                return;
                //pathIsEnded = true;
            }
            

            if (myT.position != target.position)
            {
                direction = target.position - myT.position;
                direction.Normalize();
                anim.SetFloat("DirectionX", direction.x);
                anim.SetFloat("DirectionY", direction.y);
            }
        }

      
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }

        if (path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            if(pathIsEnded)
            {
                return;
            }
            //Debug.Log("End of path reached.");
            pathIsEnded = true;
            return;
        }

        pathIsEnded = false;

        Vector3 dir1 = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        Vector3 dir = dir1 * speed * Time.fixedDeltaTime;

        //calculate the dot product here and print to log
        float dotAngle = Vector2.Dot(myT.position.normalized, dir1);
        //Debug.Log("Dot Angle: " + dotAngle);
        //Move AI
        rb.AddForce(dir, fMode);

        if(dotAngle < 0.10 && dotAngle > -0.10)
        {
            //Debug.Log("Dot Angle: " + dotAngle);
            //Debug.Log("WEEEEEEEEE");
            rb.AddForce(dir * 1.75f, fMode);
        }

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }

    private IEnumerator UpdatePath()
    {
        if (target == null)
        {
            yield break;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        IAttacker attacker = Interface.Find<IAttacker>(other.gameObject);
        if (attacker != null && attacker.Team == Team.Player)
        {
            anim.SetBool("CanSeePlayer", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Patrol", false);
            //Debug.Log("Can see the player!");
            if (!townNPC)
            {
                target = other.gameObject.transform;
            }
            seeker.StartPath(transform.position, target.position, OnPathComplete);
            StartCoroutine(UpdatePath());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        IAttacker attacker = Interface.Find<IAttacker>(other.gameObject);
        if (attacker != null && attacker.Team == Team.Player)
        {
            //anim.SetBool("CanSeePlayer", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Patrol", false);
            //Debug.Log("Run after player!");
            target = other.gameObject.transform;
            seeker.StartPath(transform.position, target.position, OnPathComplete);
            StartCoroutine(UpdatePath());
        }
    }

    public void OnPathComplete(Path p)
    {
        //Debug.Log("We got a path. Did it have an error? " + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            //anim.SetBool("CanSeePlayer", false);
        }
    }
}
