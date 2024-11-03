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
        ServiceLocator.Current.Get<EventBus>().Invoke(new PlayerTakeDamageEvent(damage));
        Debug.Log("����� ������� ����: " + damage);
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
        Debug.Log("����� �����");
        ServiceLocator.Current.Get<EventBus>().Invoke(new PlayerDiedEvent());
        // ����� ����� �������� ������ ��������� ��� ��������
    }

}
