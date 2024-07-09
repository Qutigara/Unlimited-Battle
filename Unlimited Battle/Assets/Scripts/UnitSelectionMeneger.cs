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
    private bool check;



    private Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        check = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {

                if (cam.GetComponent<CameraController>().OwnerCamera == hit.collider.gameObject.GetComponent<Unit>().OwnerUnit)
                {
                    SelectByClicking(hit.collider.gameObject);
                    check = true;
                    Debug.Log("Выбрал себя");
                }
                else if ((cam.GetComponent<CameraController>().OwnerCamera >= 0 && cam.GetComponent<CameraController>().OwnerCamera <= 3) && (hit.collider.gameObject.GetComponent<Unit>().OwnerUnit >= 0 && hit.collider.gameObject.GetComponent<Unit>().OwnerUnit <= 3) ||
                         (cam.GetComponent<CameraController>().OwnerCamera >= 4 && cam.GetComponent<CameraController>().OwnerCamera <= 7) && (hit.collider.gameObject.GetComponent<Unit>().OwnerUnit >= 4 && hit.collider.gameObject.GetComponent<Unit>().OwnerUnit <= 7) ||
                         (cam.GetComponent<CameraController>().OwnerCamera >= 8 && cam.GetComponent<CameraController>().OwnerCamera <= 11) && (hit.collider.gameObject.GetComponent<Unit>().OwnerUnit >= 8 && hit.collider.gameObject.GetComponent<Unit>().OwnerUnit <= 11)

                        )
                {
                    SelectByClickingAlly(hit.collider.gameObject);
                    Debug.Log("Выбрал союза");
                }
                else
                {
                    SelectByClickingEnemy(hit.collider.gameObject);
                    Debug.Log("Выбрал врага");
                }




            }

        }

        if (Input.GetMouseButtonDown(1) && unitsSelected.Count > 0 && check)
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
        if (unitsSelected.Count > 0 && AtleastOneOffensiveUnit(unitsSelected) && check)
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
            EnableMoveUnit(unit, false);
            if (check)
            {
                TriggerSelectionIndicator(unit, false);
            }
            else
            {
                TriggerSelectionIndicatorEnemy(unit, false);
                TriggerSelectionIndicatorAlly(unit, false);
            }
            
        }
        check = false;
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

    private void SelectByClickingEnemy(GameObject unit)
    {
        DeselectAll();
        TriggerSelectionIndicatorEnemy(unit, true);
        unitsSelected.Add(unit);

    }

    private void SelectByClickingAlly(GameObject unit)
    {
        DeselectAll();
        unitsSelected.Add(unit);
        TriggerSelectionIndicatorAlly(unit, true);
    }



    private void EnableMoveUnit(GameObject unit, bool trigger)
    {
        unit.GetComponent<MoveUnit>().enabled = trigger;
    }

    private void MultiSelect(GameObject unit)
    {
        if (unitsSelected.Contains(unit) == false)
        {
            unitsSelected.Add(unit);
            TriggerSelectionIndicator(unit, true);

            EnableMoveUnit(unit, true);
        }
        else
        {
            EnableMoveUnit(unit, false);
            TriggerSelectionIndicator(unit, false);
            unitsSelected.Remove(unit);
        }
    }

    private void TriggerSelectionIndicator(GameObject unit, bool isVisible)
    {
        unit.transform.Find("Indicator").gameObject.SetActive(isVisible);
    }

    private void TriggerSelectionIndicatorEnemy(GameObject unit, bool isVisible)
    {
        unit.transform.Find("IndicatorEnemy").gameObject.SetActive(isVisible);
    }

    private void TriggerSelectionIndicatorAlly(GameObject unit, bool isVisible)
    {
        unit.transform.Find("IndicatorAlly").gameObject.SetActive(isVisible);
    }





}
