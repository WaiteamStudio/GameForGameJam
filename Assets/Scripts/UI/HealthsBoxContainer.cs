using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
public class Health
{
    
}
public class HealthsBoxContainer : MonoBehaviour
{
    TextMeshProUGUI healthText;
    public void Awake()
    {
        HealthComponent healthComponent = ServiceLocator.Current.Get<PlayerController>().GetHealthComponent();
        if(healthComponent != null )
            healthText.text = healthComponent.GetCurrentHealth().ToString();
        else
        {
            Debug.Log("healthComponent is null");
        }    
        healthText = GetComponent<TextMeshProUGUI>();
        ServiceLocator.Current.Get<EventBus>().Subscribe<PlayerTakeDamageEvent>(OnPlayerGetDamage);
    }
    private void OnPlayerGetDamage(PlayerTakeDamageEvent @event)
    {
        healthText.text = @event.currenthealth.ToString();
    }
}
