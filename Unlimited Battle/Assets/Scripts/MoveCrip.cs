using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MoveCrip : MonoBehaviour
{

    Camera cam;
    NavMeshAgent agent;
    public LayerMask ground;
    public LayerMask attackable;
    private Animator anim;
    AttackController attackController;
    Unit caster;
    

    private Transform targetToMove;

    private void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        targetToMove = GameObject.Find("MidCripsImpactPoint").GetComponent<Transform>();
        caster = GetComponent<Unit>(); // ������������� caster
    }
    void Update()
    {

        agent.SetDestination(targetToMove.position);
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRun", true);
            anim.SetBool("isAttack", false);
            //isCommandedToMove = true;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                //isCommandedToMove = true;
                //attackController.targetToAttack = null;
                anim.SetBool("isRun", true);
                anim.SetBool("isAttack", false);

                agent.SetDestination(hit.point);

            }
        }


        if (agent.hasPath == false || agent.remainingDistance <= agent.stoppingDistance)
        {

            anim.SetBool("isRun", false);
            //anim.SetBool("isIdle", true);
            caster.isCommandedToMove = false;
        }
        else
        {
            //isCommandedToMove = true;
            anim.SetBool("isRun", true);
        }

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    isCommandedToMove = false;
        //    agent.isStopped = true;
        //    agent.ResetPath();
        //    anim.SetBool("isAttack", false);
        //    anim.SetBool("isRun", false);
        //    anim.SetBool("isIdle", true);
        //    //attackController.targetToAttack = null;

        //}
    }



}