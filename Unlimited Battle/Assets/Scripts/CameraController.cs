using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // ��������� ������
    public float moveSpeed = 10f;
    public float zoomSpeed = 2f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    public float rotateSpeed = 100f;
    public float edgeScrollSpeed = 5f; // �������� ���������� �� ����� ������
    public Vector2 panLimit;
    public int OwnerCamera;
    // ������� ������� ������
    private float currentZoom = 10f;



    // �������� ����
    void Update()
    {


        // ����������� ������
        HandleMovement();

        // ��������������� ������
        HandleZoom();


    }

    // ��������� �����������
    void HandleMovement()
    {
        // ��������� ������� ������� ���� � ������� �����������
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 pos = transform.position;

        // ��������, ��������� �� ������ �� ����� ������
        if (Input.mousePosition.x < Screen.width * 0.01f)
        {
            transform.position += Vector3.back * edgeScrollSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.x > Screen.width * 1)
        {
            transform.position += Vector3.forward * edgeScrollSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y < Screen.height * 0.01f)
        {
            transform.position += Vector3.right * edgeScrollSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.y > Screen.height * 1)
        {
            transform.position += Vector3.left * edgeScrollSpeed * Time.deltaTime;
        }



        pos.x = Mathf.Clamp(transform.position.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(transform.position.z, -panLimit.y, panLimit.y);

        transform.position = pos;

    }

    // ��������� ���������������
    void HandleZoom()
    {
        // ��������������� � ������� ������ ���������
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scrollInput * zoomSpeed;

        // ����������� ���������������
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // ���������� ���������������
        Camera.main.orthographicSize = currentZoom;
    }



}
