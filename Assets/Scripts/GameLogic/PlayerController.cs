using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IService
{
    [SerializeField] private Sprite fireFormSprite; //спрайт для огненной формы
    [SerializeField] private Sprite waterFormSprite; //спрайт для водяной формы
    private SpriteRenderer spriteRenderer; //компонент спрайт рендерер пресонажа 
    private TeleportationPoint nearTeleportationPoint = null; // Текущая точка телепортации рядом с персонажем
    HealthComponent healthComponent;
    [SerializeField]
    ParticleSystem HeadWaterPS;
    [SerializeField]
    ParticleSystem HeadFirePS;
    [SerializeField]
    ParticleSystem StepFirePS;
    [SerializeField]
    ParticleSystem StepWaterPS;
    PlrMovement PlrMovement;
    private Time lastTimeEmited;
    private Animator anim;
    private PlayerForm currentForm
    {
        get {
            PlayerHealth playerHealth = healthComponent as PlayerHealth;
            return playerHealth.GetForm();
        }
        set {
            PlayerHealth playerHealth = healthComponent as PlayerHealth;
            playerHealth.SetForm(value);
        }
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlrMovement = GetComponent<PlrMovement>();
        healthComponent = GetComponent<HealthComponent>();
        anim = GetComponent<Animator>();
        UpdateSprite();
        ServiceLocator.current.Get<EventBus>().Subscribe<PlayerDiedEvent>(OnPlayerDied);
        
    }
    private void Start()
    {
        InitParticleSystems();
    }

    private void InitParticleSystems()
    {
        SwitchParticleSystem();
    }

    private void Update()
    {
        EmitStepParticles();
    }

    private void EmitStepParticles()
    {
        if(PlrMovement.IsMoving && PlrMovement.isGrounded)
        {
            if (currentForm == PlayerForm.Fire)
            {
                if(StepFirePS.isPlaying==false)
                    StepFirePS.Play();
                StepWaterPS?.Stop();
            }
            else
            {
                if(StepWaterPS?.isPlaying == false)
                    StepWaterPS?.Play();
                StepFirePS?.Stop();
            }
        }
        else
        {
            StepFirePS?.Stop();
            StepWaterPS?.Stop();
        }
    }

    public HealthComponent GetHealthComponent()
    {
        return healthComponent;
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
        PlaySwitchFormSound();
        SwitchParticleSystem();
        Debug.Log("Персонаж сменил форму на: " + currentForm);
    }

    private void SwitchParticleSystem()
    {
        if (currentForm == PlayerForm.Fire)
        {
            HeadFirePS?.Play();
            HeadWaterPS?.Stop();
        }
        else
        {
            HeadFirePS?.Stop();
            HeadWaterPS?.Play();
        }
    }

    private void PlaySwitchFormSound()
    {
        if (currentForm == PlayerForm.Fire)
        {
            SoundManager.PlaySound(SoundManager.Sound.FormSwitchFire);
        }
        else
        { SoundManager.PlaySound(SoundManager.Sound.FormSwitchWater); }
    }

    public void UpdateSprite()
    {
        if (currentForm == PlayerForm.Fire)
        {
            anim.SetBool("isFire", true);//spriteRenderer.sprite = fireFormSprite;
        }
        else
        {
            anim.SetBool("isFire", false);//spriteRenderer.sprite = waterFormSprite;
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
                SoundManager.PlaySound(SoundManager.Sound.Teleportation);
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