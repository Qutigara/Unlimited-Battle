using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform targetToAttack;


    public Material idleStateMaterial;
    public Material runStateMaterial;
    public Material attackStateMaterial;

    public bool isPlayer;

    public int unitDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer && other.CompareTag("Enemy") && targetToAttack == null)
        {
            targetToAttack = other.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isPlayer && other.CompareTag("Enemy") && targetToAttack == null)
        {
            targetToAttack = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPlayer && other.CompareTag("Enemy") && targetToAttack != null)
        {
            targetToAttack = null;
        }
    }

    public void SetIdleMaterial()
    {
        //GetComponent<Renderer>().material = idleStateMaterial;
    }

    public void SetRunMaterial()
    {
        //GetComponent<Renderer>().material = runStateMaterial;
    }
    public void SetAttackMaterial()
    {
        //GetComponent<Renderer>().material = attackStateMaterial;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }

}