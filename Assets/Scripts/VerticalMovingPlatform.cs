using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour
{
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float moveSpeed = 1f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 lastPosition; // ������� ��������� � ���������� �����
    private Vector3 currentPlatformVelocity; // ������� �������� ���������
    private Transform playerTransform; // ��������� ������, ����� ������� ��� ������ � ����������

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0, moveDistance, 0);
        lastPosition = transform.position; // ��������� ��������� ������� ��� ���������
    }

    private void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = targetPosition == startPosition ? startPosition + new Vector3(0, moveDistance, 0) : startPosition;
        }

        // ��������� ������� �������� ��������� �� ������ ������� ������� ����� �������
        currentPlatformVelocity = (transform.position - lastPosition) / Time.deltaTime;

        // ���� ����� ��������� �� ���������, ���������� ��� �� �� �� ��������, ��� � ���������
        if (playerTransform != null)
        {
            playerTransform.position += currentPlatformVelocity * Time.deltaTime;
        }

        lastPosition = transform.position; // ��������� ��������� �������
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� ����� ������ ��� ����� �� ���������, ��������� ��� ���������
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // ���� ����� �������� ���������, �������� ��� ���������
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = null;
        }
    }
}
