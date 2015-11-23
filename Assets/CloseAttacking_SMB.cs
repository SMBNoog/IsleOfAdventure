﻿using UnityEngine;
using System.Collections;

public class CloseAttacking_SMB : StateMachineBehaviour {
    
    Player player;
    Rigidbody2D myR;
    float distance;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<Player>();
        myR = FindObjectOfType<Rigidbody2D>();      
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Enemy pos: " + animator.gameObject.transform.position);
        //Debug.Log("Player pos: " + player.Pos);
        RaycastHit2D rayToPlayer = Physics2D.Linecast(animator.gameObject.transform.position, player.Pos, 1 << 10);
        if (rayToPlayer.collider != null)
        {
            //Debug.Log("Collider hit: " + rayToPlayer.collider);
            //Debug.Log("Distance to player: " + rayToPlayer.distance);
            animator.SetFloat("DistanceFromTarget", rayToPlayer.distance);

            Vector2 direction = rayToPlayer.transform.position - myR.transform.position;
            myR.velocity = direction * 1f;
        }
        else
            Debug.Log("collider is null");
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
