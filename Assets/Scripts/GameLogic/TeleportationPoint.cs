using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationPoint : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget; //����� � ������� ����� ����������������� �����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().SetNearTeleportationPoint(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().SetNearTeleportationPoint(null); // ������� ������ �� ����� ������������, ���� �������� ����� �� �������
        }
    }

    public Transform GetTeleportTarget()
    {
        return teleportTarget;
    }
}
