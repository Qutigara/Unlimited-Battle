using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionMeneger : MonoBehaviour
{
   
    public static UnitSelectionMeneger Instance { get; set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public List<GameObject> allUnitsList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    public LayerMask clickable;
    public LayerMask ground;
    public LayerMask attackable;
    public GameObject groundMarker;
    public bool attackCursorVisible;

    


    private Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, Mathf.Infinity,clickable))
            {
                SelectByClicking(hit.collider.gameObject);
            }

        }

        if (Input.GetMouseButtonDown(1) && unitsSelected.Count > 0)
        {

            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                groundMarker.transform.position = hit.point;

                groundMarker.SetActive(false);
                groundMarker.SetActive(true);
            }

        }

        //Attack Target
        if (unitsSelected.Count > 0 && AtleastOneOffensiveUnit(unitsSelected))
        {

            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, Mathf.Infinity, attackable))
            {
                attackCursorVisible = true;

                if (Input.GetMouseButtonDown(1))
                {
                    Transform target = hit.transform;

                    foreach (GameObject unit in unitsSelected)
                    {
                        if (unit.GetComponent<AttackController>())
                        {
                            unit.GetComponent<AttackController>().targetToAttack = target;
                        }
                    }
                }
            }
            else
            {
                attackCursorVisible = false;
            }

        }

    }

    private bool AtleastOneOffensiveUnit(List<GameObject> unitsSelected) 
    {
        foreach (GameObject unit in unitsSelected)
        {
            if (unit.GetComponent<AttackController>())
            {
                return true;
            }

            
        }
        return false;
    }

    private void DeselectAll()
    {
        
        foreach (var unit in unitsSelected)
        {
            if (unit.CompareTag("Enemy"))
            {
                unit.transform.GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                EnableMoveUnit(unit, false);
                TriggerSelectionIndicator(unit, false);
            }
            
        }

        groundMarker.SetActive(false);
        unitsSelected.Clear();
    }



    private void SelectByClicking(GameObject unit)
    {
        DeselectAll();
        if (unit.CompareTag("Enemy"))
        {
            unitsSelected.Add(unit);
            unit.transform.GetChild(3).gameObject.SetActive(true);
        }
        else
        {
            unitsSelected.Add(unit);
            TriggerSelectionIndicator(unit, true);
            EnableMoveUnit(unit, true);
        }


    }

    private void EnableMoveUnit(GameObject unit, bool trigger)
    {
        unit.GetComponent<MoveUnit>().enabled = trigger;
    }

    private void MultiSelect(GameObject unit)
    {
        if(unitsSelected.Contains(unit) == false)
        {
            unitsSelected.Add(unit);
            TriggerSelectionIndicator(unit, true);
            
            EnableMoveUnit(unit,true);
        }
        else
        {
            EnableMoveUnit(unit,false);
            TriggerSelectionIndicator(unit, false);
            unitsSelected.Remove(unit);
        }
    }

    private void TriggerSelectionIndicator(GameObject unit, bool isVisible)
    {
        unit.transform.GetChild(2).gameObject.SetActive(isVisible);
    }
}
