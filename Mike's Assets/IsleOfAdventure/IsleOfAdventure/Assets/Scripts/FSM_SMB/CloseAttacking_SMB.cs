﻿using UnityEngine;
using System.Collections;

public class CloseAttacking_SMB : StateMachineBehaviour {
        
    Rigidbody2D myR;
    float distance;

    IAttacker attacker;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myR = animator.gameObject.GetComponent<Rigidbody2D>();
        attacker = Interface.Find<IAttacker>(FindObjectOfType<Player>().gameObject);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attacker != null)
        {
            //Debug.Log("Enemy pos: " + animator.gameObject.transform.position);
            //Debug.Log("Player pos: " + player.Pos);
            //RaycastHit2D rayToPlayer = Physics2D.Linecast(animator.gameObject.transform.position, attacker.Pos, 1 << 10);
            //if (rayToPlayer.collider != null)
            //{
            //Debug.Log("Collider hit: " + rayToPlayer.collider);
            //Debug.Log("Distance to player: " + rayToPlayer.distance);

            if(myR != null)
            {
                Vector3 direction = attacker.Pos - (Vector2)myR.transform.position;
                myR.velocity = direction * 60f * Time.fixedDeltaTime;
            }

            //}
            //else
            //    Debug.Log("collider is null");
        }
        else
            Debug.Log("Attacker is Null");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
