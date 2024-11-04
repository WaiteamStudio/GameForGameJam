using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 Direction;
    [SerializeField]
    public float MoveSpeed;
    [SerializeField]
    private float lifeTime = 3f; // ¬рем€ жизни пули в секундах
    [SerializeField]
    bool ScreenEdgeDestroy;
    [SerializeField]
    bool LifeTimeDestroy;
    MoveMechanic moveMechanic;

    public Bullet()
    {
        Direction = GetStartingDirection();
    }
    public Bullet(Vector3 direction)
    {
        this.Direction = direction;
    }
    void FixedUpdate()
    {
        if(ScreenEdgeDestroy)
            CheckIfOutOfScreen();
        if (moveMechanic != null)
        {
            moveMechanic.FixedUpdate();
        }
    }
    public void Init(Vector3 direction, float MoveSpeed)
    {
        ChangeDirection(direction);
        ChangeSpeed(MoveSpeed);
        moveMechanic = new MoveMechanic(direction, MoveSpeed, transform);

        // ”ничтожить объект после времени жизни
        if(LifeTimeDestroy)
            Destroy(gameObject, lifeTime);
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
        return Direction;
    }
    private Vector3 GetStartingDirection()
    {
        return new Vector3(1f, 0f);
    }
    private void CheckIfOutOfScreen()
    {
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPosition.x < 0 || screenPosition.x > 1 || screenPosition.y < 0 || screenPosition.y > 1)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    internal void SetForm(PlayerForm playerForm)
    {
        ElementalDamageDealer damageDealer = GetComponent<ElementalDamageDealer>();
        if(damageDealer != null)
            damageDealer.SetForm(playerForm);
    }
}
