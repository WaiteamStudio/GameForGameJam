using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthComponent health = collision.GetComponent<HealthComponent>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Debug.Log("Герой получил урон: " + damage);
        }
    }
}
