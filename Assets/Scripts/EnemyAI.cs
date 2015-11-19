using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RayHitType { nothing, player, wall, enemy }

public class EnemyAI : MonoBehaviour {
    
    public float distance = 10f;
    public RayHitType rayHit;
    List<RaycastHit2D> rays;

    // Update is called once per frame
    void Update()
    {
        rayHit = RayHitType.nothing;
        rays = new List<RaycastHit2D>();
        Raycasting();
    }

    void Raycasting()
    {
        float m = -1f;
        for (int i = 0; i < 8; i++)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos(m), Mathf.Sin(m)), distance, 10);
            rays.Add(ray);
            m += Mathf.PI / 8 * 10;
            //Debug.DrawRay(transform.position, new Vector2(Mathf.Cos(m), Mathf.Sin(m)), Color.green, distance);
        }        

        foreach (RaycastHit2D hit in rays)
        {          
            if(hit.collider != null /*&& hit.collider.gameObject.layer != LayerMask.NameToLayer("Enemy")*/)
            {
                Debug.Log(hit.collider.name + " Hit the Ray!");
                Debug.Log("At a distance of " + Vector2.Distance(hit.point, transform.position)); 

                if(hit.collider.name == "Player")
                    rayHit = RayHitType.player;
                if (hit.collider.name == "Wall")
                    rayHit = RayHitType.wall;
            }
        }        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(.2f, .2f, .2f, .2f);
        Gizmos.DrawSphere(transform.position, distance);

        float m = 0f;
        for(int i = 0; i<5; i++)
        {
            Gizmos.DrawRay(transform.position, new Vector2(Mathf.Cos(m), Mathf.Sin(m)));
            m += Mathf.PI / 4;
        }
    }
}
