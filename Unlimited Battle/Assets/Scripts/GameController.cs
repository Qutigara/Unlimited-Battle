using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<Unit> selectedUnitList;
    private Vector3 startPosition;
    // Start is called before the first frame update

    private void Awake()
    {
        selectedUnitList = new List<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, Input.mousePosition);

            selectedUnitList.Clear();

            foreach (Collider2D collider2D in collider2DArray)
            {
                Unit unit = collider2D.GetComponent<Unit>();
                if (unit != null)
                {
                    selectedUnitList.Add(unit);
                }
            }
        }
    }
}
