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
    public GameObject groundMarker;

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
    }

    private void DeselectAll()
    {
        
        foreach (var unit in unitsSelected)
        {
            EnableMoveUnit(unit, false);
            TriggerSelectionIndicator(unit, false);
        }

        groundMarker.SetActive(false);
        unitsSelected.Clear();
    }



    private void SelectByClicking(GameObject unit)
    {
        DeselectAll();

        unitsSelected.Add(unit);
        TriggerSelectionIndicator(unit, true);
        EnableMoveUnit(unit, true);

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
