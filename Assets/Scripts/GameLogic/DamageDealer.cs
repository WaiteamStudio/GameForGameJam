using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("adsd");
        HealthComponent health = collision.GetComponent<HealthComponent>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("adsd");
        HealthComponent health = collision.gameObject. GetComponent<HealthComponent>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Debug.Log("Герой получил урон: " + damage);
        }
    }
    protected virtual void DealDamage(HealthComponent healthComponent, int damage)
    { 
        healthComponent.TakeDamage(damage);
    }
}
