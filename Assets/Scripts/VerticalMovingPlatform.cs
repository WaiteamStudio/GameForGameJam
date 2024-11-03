using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour
{
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float moveSpeed = 1f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 lastPosition; // Позиция платформы в предыдущем кадре
    private Vector3 currentPlatformVelocity; // Текущая скорость платформы
    private Transform playerTransform; // Трансформ игрока, чтобы двигать его вместе с платформой

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0, moveDistance, 0);
        lastPosition = transform.position; // Сохраняем начальную позицию как последнюю
    }

    private void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = targetPosition == startPosition ? startPosition + new Vector3(0, moveDistance, 0) : startPosition;
        }

        // Вычисляем текущую скорость платформы на основе разницы позиции между кадрами
        currentPlatformVelocity = (transform.position - lastPosition) / Time.deltaTime;

        // Если игрок находится на платформе, перемещаем его на ту же величину, что и платформа
        if (playerTransform != null)
        {
            playerTransform.position += currentPlatformVelocity * Time.deltaTime;
        }

        lastPosition = transform.position; // Обновляем последнюю позицию
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Если игрок только что встал на платформу, фиксируем его трансформ
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Если игрок покидает платформу, обнуляем его трансформ
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = null;
        }
    }
}
