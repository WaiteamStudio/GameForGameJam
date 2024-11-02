using System;
using Unity.VisualScripting;
using UnityEngine;
public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    public int maxHealth = 100;
    [SerializeField]
    private int currentHealth;
    private int Health
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
        }
    }
    public PlayerForm currentForm = PlayerForm.Water;
    EventBus eventBus;
    private void Start()
    {
        currentHealth = maxHealth;
        eventBus = ServiceLocator.Current.Get<EventBus>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // ������� ������� ����������� �����
        {
            SwitchForm();
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        eventBus.Invoke(new PlayerTakeDamageEvent(damage));
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void TakeDamage(int damage, PlayerForm playerForm)
    {
        if (playerForm != currentForm)
        {
            TakeDamage(damage);
        }
        else
        {
            Heal(damage);
        }
    }

    private void Heal(int damage)
    {
       Health += damage;
    }

    private void Die()
    {
        eventBus.Invoke(new PlayerDiedEvent()) ;
        Debug.Log("����� �����");
        // ����� ����� �������� ������ ��������� ��� ��������
    }


    public void SwitchForm()
    {
        // ����������� ����� ������
        currentForm = currentForm == PlayerForm.Water ? PlayerForm.Fire : PlayerForm.Water;
        UpdatePlayerLayer();
        Debug.Log("����� ������: " + currentForm);
    }

    private void UpdatePlayerLayer()
    {
        // ������ ���� ������ � ����������� �� �����
        if (currentForm == PlayerForm.Fire)
        {
            gameObject.layer = LayerMask.NameToLayer("FireLayer");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("WaterLayer");
        }
    }
}
