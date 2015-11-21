using UnityEngine;
using System.Collections;

public class PatrolllingSMB : StateMachineBehaviour {

    Rigidbody2D rb;
    float x, y;
    Vector3 lastPos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        rb = animator.gameObject.GetComponent<Rigidbody2D>();
        x = Random.Range(-1f, 1f);
        y = Random.Range(-1f, 1f);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Patrolling
        Debug.Log("X: " + x + "Y: " + y);
        rb.velocity = (new Vector2(x, y) * 1f).normalized;

        if (rb.transform.position != lastPos);
        {
            Vector3 direction = rb.transform.position - lastPos;
            direction.Normalize();
            animator.SetFloat("DirectionX", direction.x);
            animator.SetFloat("DirectionY", direction.y);
        }
        lastPos = rb.transform.position;
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
