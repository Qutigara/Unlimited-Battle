using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    void Start()
    {
        UnitSelectionMeneger.Instance.allUnitsList.Add(gameObject);
    }

    private void OnDestroy()
    {
        UnitSelectionMeneger.Instance.allUnitsList.Remove(gameObject);
    }
}
