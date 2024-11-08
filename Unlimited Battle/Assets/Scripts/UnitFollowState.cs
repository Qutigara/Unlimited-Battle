using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitFollowState : StateMachineBehaviour
{

    AttackController attackController;
    Unit caster;
    NavMeshAgent agent;
    public float attackingDistanse = 1.7f;
    public float rotationSpeed = 5f;
    private float distanceFromTarget;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackController = animator.transform.GetComponent<AttackController>();
        agent = animator.transform.GetComponent<NavMeshAgent>();
        if (agent != null && !agent.enabled)
        {
            // If agent is not enabled, enable it
            agent.enabled = true;
        }
        attackController.SetRunMaterial();
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Should Unit Transition to Idle State?
        if (attackController.targetToAttack == null && animator.transform.GetComponent<Unit>().isCommandedToMove == false)
        {
            animator.SetBool("isRun", false);
            //  Debug.Log("1");
        }
        else
        {


            if (animator.transform.GetComponent<Unit>().isCommandedToMove == false && agent != null  && animator != null)
            {

                //Should Unit Transition to Attack State?

                distanceFromTarget = Vector3.Distance(attackController.targetToAttack.position, animator.transform.position);
                if (distanceFromTarget < attackingDistanse)
                {

                    

                    if (animator != null)
                    {
                        agent.SetDestination(animator.transform.position);
                    }

                    animator.SetBool("isAttack", true);// Move to Attacking State
                    //animator.SetBool("isIdle", true);
                    animator.SetBool("isRun", false);

                    
                }
                else
                {
                    // Moving Unit toward Enemy

                     
                    //animator.SetBool("isRun", true);
                    animator.SetBool("isAttack", false);
                    if (animator != null)
                    {
                        agent.SetDestination(attackController.targetToAttack.position);
                    }
                    
                    //animator.transform.LookAt(attackController.targetToAttack);

                    Vector3 direction = attackController.targetToAttack.position - animator.transform.position;
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    animator.transform.rotation = Quaternion.Lerp(animator.transform.rotation, rotation, rotationSpeed * Time.deltaTime);


                }
            }
        }
    }
}






















































