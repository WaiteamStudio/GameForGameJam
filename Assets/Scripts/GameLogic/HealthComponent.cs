using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    public int maxHealth = 100;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private PlayerForm currentForm;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Герой получил урон: " + damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void TakeDamage(int damage, PlayerForm form)
    {
        if(form!= currentForm)
        {
            TakeDamage(damage);
        }
    }

    private void Die()
    {
        Debug.Log("Герой погиб");
        // Здесь можно добавить логику проигрыша или респауна
    }
}
