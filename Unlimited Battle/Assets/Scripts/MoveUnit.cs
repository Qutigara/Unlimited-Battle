using UnityEngine;
using UnityEngine.AI;

public class MoveUnit : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent agent;
    private Animator anim;
    private bool isAtDestination;


    private void Start()
    {

        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        isAtDestination = true;

    }
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            agent.isStopped = false;
            anim.SetFloat("Speed", 0.5f);
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                anim.SetFloat("Speed", 0.5f);
                agent.SetDestination(hit.point);
                isAtDestination = false;
                Debug.Log("Проиграна анимация walk");
                Debug.Log("destination = " + agent.destination);
                // Debug.Log("remainingDistance = " );
                Debug.Log("remainingDistance = " + agent.remainingDistance);
                //Debug.Log("stoppingDistance = " + agent.stoppingDistance);
            }
        }

        // Проверка, достигла ли цель назначения
        if (agent.remainingDistance <= 0)
        {
            if (!isAtDestination)
            {
                // Вызов анимации после достижения цели
                anim.SetFloat("Speed", 1);
                isAtDestination = true;
                Debug.Log("Проиграна анимация stand");
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            agent.isStopped = true;
            agent.ResetPath();

            anim.SetFloat("Speed", 1);
            isAtDestination = true;
            Debug.Log("Проиграна анимация stand");
        }
    }

}