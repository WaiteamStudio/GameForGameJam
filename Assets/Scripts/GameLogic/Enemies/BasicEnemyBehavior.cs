using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BasicEnemyBehavior : MonoBehaviour
{   
    //list of waypoints
    public List<Transform> points;
    public int nextID = 0; // next point index
    private int idChangeValue = 1; //applied to id for changing
    //Speed of movement of flying

    public bool isFire = true; //element

    public float moveSpeed = 1.5f;
    public bool dead = false;
    private int enemyState = 0; //action state

    public float idleTime = 1f;
    private float idleTimer = 0; //countdown till returning to roaming
    public bool doesIdle = true;

    private GameObject player;

    //checking horisontal and vertical distance to player and when it should react
    public float distToPlayerHor = 5;
    public float distToPlayerVert = 5;

    //checking how close the player should be to the point to count it as "reached"
    public float distToPoint = 0.3f;
    private Animator anim;

    private void Reset()
    {
        Init();
    }

    void Init()
    {
        //GetComponent<BoxCollider2D>().isTrigger = false; //true
        GetComponent<Rigidbody2D>().freezeRotation = true;
        //Create Root Object
        GameObject root = new GameObject(name + "_Root");
        //Reset Position of Root to enemy object
        root.transform.position = transform.position;
        //Set enemy as a child of root
        transform.SetParent(root.transform);
        //Create waypoints object
        GameObject waypoints = new GameObject("Waypoints");
        //reset waypoints object position
        //make waypoints child of root
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;
        //create two points (gameobject) and reset position to waypoints object
        //make points children of waypoints
        GameObject p1 = new GameObject("Point1"); p1.transform.SetParent(waypoints.transform); p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point2"); p2.transform.SetParent(waypoints.transform); p2.transform.position = root.transform.position;

        //Init points list and add points
        points = new List<Transform>
        {
            p1.transform,
            p2.transform
        };
    }

    /* void Start()
     {
         idleTime = idleTime*Time.time;
     }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dead){
            anim.SetTrigger("dead");
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("end anim"))
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
        else
        switch (enemyState)
        {
            
            case 0: //idle
                anim.SetBool("iswalking", false);
                idleTimer -= Time.deltaTime;
                if (idleTimer < 0) enemyState = 1;
                PlayerTrigger();
                break;

            case 1: //patrol
                anim.SetBool("iswalking", true);    
                MoveToNextPoint();
                PlayerTrigger();
                break;

            case 2: //angy
                anim.SetBool("iswalking", true);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), moveSpeed * Time.deltaTime);
                if (player.transform.position.x > transform.position.x)
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                else transform.localScale = new Vector2(1, 1);
                if (Vector2.Distance(transform.position, new Vector2(player.transform.position.x, transform.position.y)) > distToPlayerHor || Vector2.Distance(transform.position, new Vector2(transform.position.x, player.transform.position.y)) > distToPlayerVert)
                {
                    enemyState = 0;
                    idleTimer = idleTime;
                }
                else if (Vector2.Distance(transform.position, player.transform.position) < 1)
                {
                    DeathEnemy();
                }
                break;

        }

        //something something platform boundaries

    }

    private void PlayerTrigger()
    {
        if (Vector2.Distance(transform.position, new Vector2(player.transform.position.x, transform.position.y)) < distToPlayerHor && Vector2.Distance(transform.position, new Vector2(transform.position.x, player.transform.position.y)) < distToPlayerVert)
        {
            enemyState = 2;
        }
    }
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        anim.SetBool("isFire", isFire);
    }

    private void DeathEnemy()
    {
        //Destroy(gameObject.transform.parent.gameObject);
        //Destroy(gameObject);
        dead = true;
    }

    void MoveToNextPoint()
    {
        //Get the bext point transform
        Transform goalPoint = points[nextID];
        //Flip enemy transform to look into point's direction
        if (goalPoint.transform.position.x>transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else transform.localScale = new Vector2(1, 1);
        //Move the enemy towards towards the point
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.transform.position,moveSpeed*Time.deltaTime);
        //автор - Влад
        PlayMoveSound();
        //Check distance between enemy and goal point to trigger next point
        if (Vector2.Distance(transform.position, goalPoint.transform.position) < distToPoint)
        {
            //check if reached end (-1)
            if (nextID == points.Count - 1) {
                idChangeValue = -1;
                //if (doesIdle) { enemyState = 0; idleTimer = idleTime;}
            }
            //check if reached start (+1)
            if (nextID == 0)
            {
                idChangeValue = 1;
                //if (doesIdle) { enemyState = 0; idleTimer = idleTime; }
            }
            //aply change to nextID
            if (doesIdle) { enemyState = 0; idleTimer = idleTime; }
            nextID += idChangeValue;
        }
    }
    private void PlayMoveSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.EnemyMove);
    }
}
