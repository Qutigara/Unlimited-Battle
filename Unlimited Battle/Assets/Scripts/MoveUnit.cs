using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MoveUnit : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent agent;

    private Vector3 targetPosition;


    private void Start()
    {
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit) )
            {
                 agent.SetDestination(hit.point);
            }
        }
    }
}
