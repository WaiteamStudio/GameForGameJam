using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMechanic
{
    private Vector2 movementVector;
    private float movementSpeed;
    private Transform transform;
    public MoveMechanic(Vector2 MovementVector, float MovementSpeed,Transform Transform)
    {
        movementVector = MovementVector.normalized;
        movementSpeed = MovementSpeed;
        transform = Transform;
    }
    public void FixedUpdate()
    {
        Vector2 newPos = transform.position + new Vector3(movementSpeed * Time.deltaTime * movementVector.x, movementSpeed * Time.deltaTime * movementVector.y, 0);
        transform.position = newPos;
    }
    public void ChangeMoveSpeed(float moveSpeed)
    {
        movementSpeed = moveSpeed;
    }
    public float GetMoveSpeed()
    {
        return movementSpeed;
    }
}
