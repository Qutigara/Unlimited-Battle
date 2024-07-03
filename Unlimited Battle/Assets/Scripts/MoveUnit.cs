using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveUnit : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent agent;
    private Animator anim;
    private bool isAtDestination;
    private bool isAtDestination2;


    private void Start()
    {
        
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isAtDestination2 = true;
            anim.SetFloat("Speed", 1);
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                anim.SetFloat("Speed", 1);
                agent.SetDestination(hit.point);
                isAtDestination = false;
                Debug.Log("Проиграна анимация walk");
            }
        }

        if (isAtDestination2)
        {
            // Проверка, достигла ли цель назначения
            if (agent.remainingDistance <= agent.stoppingDistance)
            {

                if (!isAtDestination)
                {
                    isAtDestination2 = false;
                    Debug.Log("Проиграна анимация stand");
                    // Вызов анимации после достижения цели
                    anim.SetFloat("Speed", 0);
                    isAtDestination = true;
                }
            }
        }
    }




}
