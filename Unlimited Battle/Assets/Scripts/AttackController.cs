using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform targetToAttack;

    Unit caster;
    public Material idleStateMaterial;
    public Material runStateMaterial;
    public Material attackStateMaterial;

    public bool isPlayer;

    public int unitDamage;

    private bool CheckOwnerTarget(Collider other)
    {

        if (other.GetComponent<Unit>() != null)
        {
            // Проверка, принадлежат ли caster и other к одной из трех групп игроков
            if ((caster.OwnerUnit >= 0 && caster.OwnerUnit <= 3) && (other.GetComponent<Unit>().OwnerUnit >= 0 && other.GetComponent<Unit>().OwnerUnit <= 3) ||
                (caster.OwnerUnit >= 4 && caster.OwnerUnit <= 7) && (other.GetComponent<Unit>().OwnerUnit >= 4 && other.GetComponent<Unit>().OwnerUnit <= 7) ||
                (caster.OwnerUnit >= 8 && caster.OwnerUnit <= 11) && (other.GetComponent<Unit>().OwnerUnit >= 8 && other.GetComponent<Unit>().OwnerUnit <= 11))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false; // Или выполните другое действие, если компонент Unit отсутствует
        }

    }

    private void Start()
    {
        caster = GetComponent<Unit>(); // Инициализация caster
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer && other.CompareTag("Player") && targetToAttack == null)
        {
            if (CheckOwnerTarget(other))
            {
              
                targetToAttack = other.transform;
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isPlayer && other.CompareTag("Player") && targetToAttack == null)
        {
            if (CheckOwnerTarget(other))
            {
                targetToAttack = other.transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPlayer && CheckOwnerTarget(other) && targetToAttack != null)
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
        Gizmos.DrawWireSphere(transform.position, 5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }



}
