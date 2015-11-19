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

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        myT = GetComponent<Transform>();
        anim = GetComponent<Animator>();

        if(target == null)
        {
            Debug.LogError("No Player found");
            return;
        }       

        //StartCoroutine(UpdatePath());
    }

    void Update()
    {
        RaycastHit2D rayToPlayer = Physics2D.Linecast(transform.position, target.position, 1 << 10);
        

        if (myT.position != target.position)
        {
            direction = target.position - myT.position;
            direction.Normalize();
            anim.SetFloat("DirectionX", direction.x);
            anim.SetFloat("DirectionY", direction.y);
        }        
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            target = FindObjectOfType<Player>().gameObject.transform;
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
            Debug.Log("End of path reached.");
            pathIsEnded = true;
            return;
        }

        pathIsEnded = false;

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //calculate the dot product here and print to log

        //Move AI        
        rb.AddForce(dir, fMode);
        //rb.velocity = dir;

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
            target = FindObjectOfType<Player>().gameObject.transform;
            yield return false;
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
            target = other.gameObject.transform;
            seeker.StartPath(transform.position, target.position, OnPathComplete);
            StartCoroutine(UpdatePath());
        }
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("We got a path. Did it have an error? " + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    



}
