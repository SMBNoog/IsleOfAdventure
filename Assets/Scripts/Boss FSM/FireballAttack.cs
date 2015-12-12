using UnityEngine;
using System.Collections;

public class FireballAttack : StateMachineBehaviour
{
    bool movingRight = true;
    public float targetSpeed = 50f;
    private Rigidbody2D myrigidbody2D;
    public float wallTestDistance = 0.1f;
    private Transform transformw;
    private Vector2 centerPoint1 = new Vector2(20f, 108.5f);
    private Vector2 centerPoint2 = new Vector2(0f, 108.5f);
    private Vector2 velocity;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myrigidbody2D = BossSingleton.instance.gameObject.GetComponent<Rigidbody2D>();
        transformw = BossSingleton.instance.gameObject.GetComponent<Transform>();
        velocity = myrigidbody2D.velocity;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (movingRight && velocity.x < targetSpeed)
            myrigidbody2D.MovePosition(transformw.position + Vector3.right * Time.deltaTime);
        else if (!movingRight && velocity.x > -targetSpeed)
            myrigidbody2D.MovePosition(transformw.position + -Vector3.right * Time.deltaTime);

        if (movingRight)
        {
            RaycastHit2D hit = Physics2D.Raycast(centerPoint1, Vector2.right, wallTestDistance);
            if (hit.collider != null)
            {
                movingRight = false;
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(centerPoint2, -Vector2.right, wallTestDistance);
            if (hit.collider != null)
            {
                movingRight = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}