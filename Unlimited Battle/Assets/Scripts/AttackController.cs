using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform targetToAttack;
    



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")  && targetToAttack != null)
        {
            targetToAttack = other.transform;
        }
    }

    private void OnTriggerEnterExit(Collider other)
    {
        if (other.CompareTag("Enemy") && targetToAttack != null)
        {
            targetToAttack = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
