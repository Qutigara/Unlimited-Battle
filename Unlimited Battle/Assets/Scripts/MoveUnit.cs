using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveUnit : MonoBehaviour
{
    // �������� ������������ �����
    public float moveSpeed = 5f;

    // ������� ����� ��� ������������
    private Vector3 targetPosition;

    // ����, �����������, ��������� �� ���� � ��������
    private bool isMoving = false;

    void Update()
    {
        // �������� ������� ������ ������ ����
        if (Input.GetMouseButtonDown(1))
        {
            // ��������� ������� ������� ���� � ������� �����������
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ��������, ����� �� ��� �� ����������
            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;

                // ��������� �����, ��� ���� ��������� � ��������
                isMoving = true;
            }
        }

        // ����������� ����� � ����, ���� �� � ��������
        if (isMoving)
        {
            // �������� ����� � ����
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // �������� ���������� ����
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // ��������� �����, ��� ���� �� � ��������
                isMoving = false;
            }
        }
    }
}
