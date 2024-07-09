using UnityEngine;
using UnityEngine.AI;

public class MoveUnit : MonoBehaviour
{

    Camera cam;
    NavMeshAgent agent;
    public LayerMask ground;
    public LayerMask attackable;
    private Animator anim;
    AttackController attackController;
    Unit caster;

    //public bool isCommandedToMove;

    private void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        caster = GetComponent<Unit>(); // Инициализация caster

    }
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRun", true);
            anim.SetBool("isAttack", false);


            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                caster.isCommandedToMove = true;
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


    }



}