using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationPoint : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget; //точка в которую будет телепортироваться игрок

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlrInput>().SetNearTeleportationPoint(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlrInput>().SetNearTeleportationPoint(null); // Убираем ссылку на точку телепортации, если персонаж вышел из области
        }
    }

    public Transform GetTeleportTarget()
    {
        return teleportTarget;
    }
}
