using UnityEngine;
using UnityEngine.AI;

public class MoveUnit : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent agent;
    private Animator anim;
    private bool isAtDestination;
    private bool isClicked;


    private void Start()
    {

        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        isAtDestination = true;
        //isClicked = true;

    }
    void Update()
    {
        Run();

        Idle();
       
        StopMoving();
    }

    private void Run()
    {
        if (Input.GetMouseButton(1))
        {
            //isClicked = false;
            agent.isStopped = false;
            RunAnimation();
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                RunAnimation();
                agent.SetDestination(hit.point);
                isAtDestination = false;
                Debug.Log("Проиграна анимация walk");
                Debug.Log("destination = " + agent.destination);
                // Debug.Log("remainingDistance = " );
                Debug.Log("remainingDistance = " + agent.remainingDistance);
                //Debug.Log("stoppingDistance = " + agent.stoppingDistance);
            }
        }
        //if (Input.GetMouseButtonUp(1) & !isClicked) { isClicked = true; }
    }

    private void Idle()
    {
        // Проверка, достигла ли цель назначения
        if (agent.remainingDistance <= 0)
        {
            if (!isAtDestination)
            {
                // Вызов анимации после достижения цели
                IdleAnimation();
                isAtDestination = true;
                Debug.Log("Проиграна анимация stand");
            }
        }
    }
    private void StopMoving()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            agent.isStopped = true;
            agent.ResetPath();

            IdleAnimation();
            isAtDestination = true;
            Debug.Log("Проиграна анимация stand");
        }
    }

    private void IdleAnimation()
    {
        anim.SetBool("isRun", false);
    }

    private void RunAnimation()
    {
        anim.SetBool("isRun", true);
    }

}