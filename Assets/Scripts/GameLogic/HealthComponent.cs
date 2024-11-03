using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    public int maxHealth = 100;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private PlayerForm currentForm;
    private EntityType entityType;
    enum EntityType
    {
        player,
        monster
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        ServiceLocator.Current.Get<EventBus>().Invoke(new PlayerTakeDamageEvent(damage));
        Debug.Log("Сущнссть получила урон: " + damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void TakeDamage(int damage, PlayerForm form)
    {
        if(form==PlayerForm.none)
        {
            TakeDamage(damage);
        }
        else if(form!= currentForm)
        {
            TakeDamage(damage);
        }
    }

    private void Die()
    {
        if (entityType == EntityType.player)
        {
            Debug.Log("Герой погиб");
            ServiceLocator.Current.Get<EventBus>().Invoke(new PlayerDiedEvent());
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Монстр погиб");
            gameObject.SetActive(false);
        }
    }
    private void SwitchForm()
    {
        currentForm = currentForm == PlayerForm.Fire ? PlayerForm.Water : PlayerForm.Fire;
        Debug.Log("Персонаж сменил форму на: " + currentForm);
    }
    public void SetForm(PlayerForm form)
    {
        currentForm = form;
    }
    public PlayerForm GetForm()
    {
        return currentForm;
    }
}
