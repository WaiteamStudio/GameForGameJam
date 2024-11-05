using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    protected int currentHealth;
    [SerializeField] protected float invulnerabilityTime;
    protected float lastTimeDamaged;
    [SerializeField]
    protected PlayerForm currentForm;
    [SerializeField]
    ParticleSystem GetHitParticlesFog;
    [SerializeField]
    SteamEffectOnCollision SteamEffectOnCollision;
    private bool dead = false;
    private Animator anim;
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        SteamEffectOnCollision = GetComponent<SteamEffectOnCollision>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        if (dead)
        {
            anim.SetTrigger("dead");
            if (anim.GetAnimatorTransitionInfo(0).IsName("end anim"))
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }

    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public void TakeDamage(int damage)
    {
        TakeDamage( damage, out bool damaged);
    }

    public void TakeDamage(int damage, out bool damaged)
    {
        if (IsInvulnerable())
        {
            Debug.Log($"Неуязвимость активна, урон {damage} не нанесен.");
            damaged = false;
            return;
        }

        ApplyDamage(damage);
        lastTimeDamaged = Time.time;
        damaged = true;
    }

    public void TakeDamage(int damage, PlayerForm enemyForm)
    {
        TakeDamage(damage, enemyForm, out bool damaged);
    }
    public void TakeDamage(DamageParameters damageParameters)
    {
        TakeDamage(damageParameters.damage, damageParameters.enemyForm, out bool damaged);
    }
    public void TakeDamage(DamageParameters damageParameters, out bool damaged)
    {
        TakeDamage(damageParameters.damage, damageParameters.enemyForm, out damaged);
        if(damaged)
        {
            SteamEffectOnCollision?.Emit(damageParameters.enemyCollision);
        }
    }

    public void TakeDamage(int damage, PlayerForm enemyForm, out bool damaged)
    {
        if (enemyForm == PlayerForm.none)
        {
            TakeDamage(damage, out damaged);
        }
        else if (enemyForm != currentForm)
        {
            TakeDamage(damage, out damaged);
        }
        else
        {
            damaged = false;
        }
    }

    private void PlayDamageSound()
    {
        if (currentForm == PlayerForm.Fire)
        {
            SoundManager.PlaySound(SoundManager.Sound.PlayerGetDamagedFireForm);
        }
        else if (currentForm == PlayerForm.Water)
        {
            SoundManager.PlaySound(SoundManager.Sound.PlayerGetDamagedWaterForm);
        }
    }

    protected bool IsInvulnerable()
    {
        return lastTimeDamaged + invulnerabilityTime > Time.time;
    }

    protected virtual void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        PlayDamageSound();
        Debug.Log($"Сущность {gameObject.name} получила урон: " + damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public PlayerForm GetForm()
    {
        return currentForm;
    }

    public void SetForm(PlayerForm form)
    {
        currentForm = form;
    }
    protected virtual void Die()
    {
        //gameObject.SetActive(false);
        dead = true;
    }
}
