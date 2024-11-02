using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 Direction;
    //[SerializeField]
    //int Damage;
    //private GameObject rifleMan;//owner,player,boss,host,parentm,wallah
    Mover mover;
    public Bullet ()
    {
        Direction = GetStartingDirection();
    }
    public Bullet(Vector3 direction)
    {
       this.Direction = direction;
    }
    void Start()
    {

    }
    public void ChangeDirection(Vector3 vector)
    {
        Direction = vector;
    }
    private void Awake()
    {
        mover = GetComponent<Mover>();
    }   
    void Update()
    {
        mover.MovementVector=MoveDirection();
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
