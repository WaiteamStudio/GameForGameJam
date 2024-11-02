using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Vector2 MovementVector {  get; set; }
    [SerializeField]
    public float movementSpeed;

    private void FixedUpdate()
    {
        Vector2 newPos = transform.localPosition + new Vector3(movementSpeed * Time.deltaTime * MovementVector.x, movementSpeed * Time.deltaTime * MovementVector.y, 0);
        transform.localPosition = newPos;
    }
}
