using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Sprite fireFormSprite; //спрайт для огненной формы
    [SerializeField] private Sprite waterFormSprite; //спрайт для водяной формы
    private SpriteRenderer spriteRenderer; //компонент спрайт рендерер пресонажа 
    private TeleportationPoint nearTeleportationPoint = null; // Текущая точка телепортации рядом с персонажем
    HealthComponent healthComponent;
    private PlayerForm currentForm
    {
        get {
            return healthComponent.GetForm();
        }
        set {
            healthComponent.SetForm(value);
                }
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthComponent = GetComponent<HealthComponent>();
        UpdateSprite();
        ServiceLocator.current.Get<EventBus>().Subscribe<PlayerDiedEvent>(OnPlayerDied);
    }

    private void OnPlayerDied(PlayerDiedEvent @event)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.isKinematic = true;
    }

    public void SwitchForm()
    {
        currentForm = currentForm == PlayerForm.Fire ? PlayerForm.Water : PlayerForm.Fire;
        UpdateSprite();
        Debug.Log("Персонаж сменил форму на: " + currentForm);
    }

    public void UpdateSprite()
    {
        if (currentForm == PlayerForm.Fire)
        {
            spriteRenderer.sprite = fireFormSprite;
        }
        else
        {
            spriteRenderer.sprite = waterFormSprite;
        }
    }

    public void SetNearTeleportationPoint(TeleportationPoint teleportPoint)
    {
        nearTeleportationPoint = teleportPoint;
    }

    public void TryTeleport()
    {
        if (currentForm == PlayerForm.Water && nearTeleportationPoint != null)
        {
            UnityEngine.Transform target = nearTeleportationPoint.GetTeleportTarget();
            if (target != null)
            {
                transform.position = target.position; // Телепортация к точке
                Debug.Log("Персонаж телепортировался!");
            }
        }
        else
        {
            Debug.Log("Телепортация невозможна: либо персонаж не в форме воды, либо рядом нет точки телепортации.");
        }
    }
}