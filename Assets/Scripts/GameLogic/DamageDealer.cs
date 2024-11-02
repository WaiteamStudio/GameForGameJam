using System.Reflection;
using UnityEngine;

public partial class DamageDealer : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthComponent health = collision.GetComponent<HealthComponent>();
        if (health != null)
        {

            DealDamage(health, damage);
            Debug.Log("Герой получил урон: " + damage);
        }
    }
    protected virtual void DealDamage(HealthComponent healthComponent, int damage)
    {
        healthComponent.TakeDamage(damage);
    }
}