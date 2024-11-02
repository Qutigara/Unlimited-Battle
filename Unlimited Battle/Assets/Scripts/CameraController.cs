using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Параметры камеры
    public float moveSpeed = 10f;
    public float zoomSpeed = 2f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    public float rotateSpeed = 100f;
    public float edgeScrollSpeed = 5f; // Скорость скроллинга по краям экрана
    public Vector2 panLimit;
    public int OwnerCamera;
    // Текущий масштаб камеры
    private float currentZoom = 10f;



    // Основной цикл
    void Update()
    {


        // Перемещение камеры
        HandleMovement();

        // Масштабирование камеры
        HandleZoom();


    }

    // Обработка перемещения
    void HandleMovement()
    {
        // Получение позиции курсора мыши в мировых координатах
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 pos = transform.position;

        // Проверка, находится ли курсор на краях экрана
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

    // Обработка масштабирования
    void HandleZoom()
    {
        // Масштабирование с помощью колеса прокрутки
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scrollInput * zoomSpeed;

        // Ограничение масштабирования
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Применение масштабирования
        Camera.main.orthographicSize = currentZoom;
    }



}
