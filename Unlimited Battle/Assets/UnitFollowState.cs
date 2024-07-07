using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitFollowState : StateMachineBehaviour
{

    AttackController attackController;

    NavMeshAgent agent;
    public float attackingDistanse = 1.7f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackController = animator.transform.GetComponent<AttackController>();
        agent = animator.transform.GetComponent<NavMeshAgent>();
        attackController.SetRunMaterial();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Should Unit Transition to Idle State?
        if (attackController.targetToAttack == null)
        {
            //animator.SetBool("isRun", false);
          //  Debug.Log("1");
        }
        else
        {
            
            if (animator.transform.GetComponent<MoveUnit>().isCommandedToMove == false)
            {
                

                //Should Unit Transition to Attack State?
                float distanceFromTarget = Vector3.Distance(attackController.targetToAttack.position, animator.transform.position);
                if (distanceFromTarget < attackingDistanse)
                {
                    
                    //agent.SetDestination(animator.transform.position);
                    animator.SetBool("isAttack", true);// Move to Attacking State

                }
                else
                {
                    // Moving Unit toward Enemy
                    Debug.Log("1");
                    animator.SetBool("isRun", true);
                    animator.SetBool("isAttack", false);
                    agent.SetDestination(attackController.targetToAttack.position);
                    animator.transform.LookAt(attackController.targetToAttack);
                    


                }
            }
            else
            {
                Debug.Log("������");
            }
        }


       
          

    }





}