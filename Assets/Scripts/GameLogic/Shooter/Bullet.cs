using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 Direction;
    [SerializeField]
    public float MoveSpeed;
    //[SerializeField]
    //int Damage;
    //private GameObject rifleMan;//owner,player,boss,host,parentm,wallah
    MoveMechanic moveMechanic;
    public Bullet ()
    {
        Direction = GetStartingDirection();
    }
    public Bullet(Vector3 direction)
    {
       this.Direction = direction;
    }
    void FixedUpdate()
    {
        if(moveMechanic!=null)
        {
            moveMechanic.FixedUpdate();
        }
    }
    public void Init(Vector3 direction,float MoveSpeed)
    {
        ChangeDirection(direction);
        ChangeSpeed(MoveSpeed);
        moveMechanic = new MoveMechanic(direction, MoveSpeed, transform);
    }
    public void ChangeDirection(Vector3 vector)
    {
        Direction = vector;
    }
    public Vector3 GetDirection()
    {
        return Direction;
    }
    public void ChangeSpeed(float speed)
    {
        MoveSpeed = speed;
    }
    public float GetSpeed()
    {
        return MoveSpeed;
    }
    private Vector2 MoveDirection()
    {
        //Debug.Log($"Bullet ({gameObject.GetInstanceID()}) is Moving");
        return Direction;
    }
    private Vector3 GetStartingDirection()
    {
        return new Vector3(1f,0f);
    }
}
