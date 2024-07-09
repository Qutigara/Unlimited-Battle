using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class UnitAttackState : StateMachineBehaviour
{

    NavMeshAgent agent;
    AttackController attackController;

    public float stopAttackingDistance = 2f;

    public float attackRate = 0.2f;
    private float attackTimer;
    private bool animationattack;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        attackController = animator.GetComponent<AttackController>();
        attackController.SetAttackMaterial();
        animationattack = true;
        attackTimer = 1f / attackRate;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attackController.targetToAttack != null && animator.transform.GetComponent<Unit>().isCommandedToMove == false)
        {
            LookAtTarget();


            //agent.SetDestination(attackController.targetToAttack.position);
            //animator.SetBool("isIdle", false);
            //animator.SetBool("isRun", false);
            if (attackTimer <= 0)
            {

                animationattack = true;
                animator.SetBool("isAttack", false);
                Attack();
                attackTimer = 1f / attackRate;
            }
            else
            {

                if (animationattack)
                {
                    animator.SetBool("isAttack", true);
                    animationattack = false;
                }

                attackTimer -= Time.deltaTime;
            }

            float distanceFromTarget = Vector3.Distance(attackController.targetToAttack.position, animator.transform.position);
            if (distanceFromTarget > stopAttackingDistance || attackController.targetToAttack == null)
            {
                // Debug.Log("4");

                animator.SetBool("isAttack", false);
                animator.SetBool("isRun", true);
                // Move to Attacking State

            }
        }
        else
        {
            animator.SetBool("isAttack", false);
        }
    }


    private void Attack()
    {


        var damageToInflict = attackController.unitDamage;

        attackController.targetToAttack.GetComponent<Unit>().TakeDamage(damageToInflict);



    }


    private void LookAtTarget()
    {
        Vector3 direction = attackController.targetToAttack.position - agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = agent.transform.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
