using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveUnit : MonoBehaviour
{
    // Скорость передвижения юнита
    public float moveSpeed = 5f;

    // Целевая точка для передвижения
    private Vector3 targetPosition;

    // Флаг, указывающий, находится ли юнит в движении
    private bool isMoving = false;

    void Update()
    {
        // Проверка нажатия правой кнопки мыши
        if (Input.GetMouseButtonDown(1))
        {
            // Получение позиции курсора мыши в мировых координатах
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Проверка, попал ли луч по коллайдеру
            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;

                // Установка флага, что юнит находится в движении
                isMoving = true;
            }
        }

        // Перемещение юнита к цели, если он в движении
        if (isMoving)
        {
            // Движение юнита к цели
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Проверка достижения цели
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // Установка флага, что юнит не в движении
                isMoving = false;
            }
        }
    }
}
