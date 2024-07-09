using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private float unitHealth;
    public float unitMaxHealth;
    public int OwnerUnit;
    public bool isCommandedToMove;

    public HealthTracker healthTracker;

    void Start()
    {
        UnitSelectionMeneger.Instance.allUnitsList.Add(gameObject);
        unitHealth = unitMaxHealth;
        UpdateHealthUI();
    }

    private void OnDestroy()
    {
        UnitSelectionMeneger.Instance.allUnitsList.Remove(gameObject);
    }

    internal void TakeDamage(int damageToInflict)
    {
        unitHealth -= damageToInflict;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(unitHealth, unitMaxHealth);

        if (unitHealth < 0)
        {
            //Dying Logic


            //Destruction or Dying Animation


            //Dying Sound Effect
            Destroy(gameObject);
        }
    }



}
