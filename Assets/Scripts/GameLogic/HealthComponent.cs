using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    public int maxHealth = 100;
    [SerializeField]
    public int currentHealth;
    [SerializeField]
    private PlayerForm currentForm;
    [SerializeField]
    private EntityType entityType;
    [SerializeField]
    float invulnerabilityTime;
    private float lastTimeDamaged;
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
        PlayGetDamageSound();
        currentHealth -= damage;
        lastTimeDamaged = Time.time;
        ServiceLocator.Current.Get<EventBus>().Invoke(new PlayerTakeDamageEvent(damage, currentHealth));
        Debug.Log($"Сущнссть {gameObject.name} получила урон: " + damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void PlayGetDamageSound()
    {
        if (entityType == EntityType.player)
        {
            if(currentForm == PlayerForm.Fire)
                SoundManager.PlaySound(SoundManager.Sound.PlayerGetDamagedFireForm);
            if(currentForm == PlayerForm.Water)
                SoundManager.PlaySound(SoundManager.Sound.PlayerGetDamagedWaterForm);
        }
        else
        {
            SoundManager.PlaySound(SoundManager.Sound.EnemyGetDamaged);

        }
    }

    public void TakeDamage(int damage, out bool damaged)
    {
        if (lastTimeDamaged + invulnerabilityTime > Time.time)
        {
            Debug.Log($"В Сущнссть должна была получить урон {damage}, но не уязвима ещё  {lastTimeDamaged + invulnerabilityTime - Time.time} " );
            damaged = false;
            SoundManager.PlaySound(SoundManager.Sound.PlayerGetDamagedFireForm);
            return;
        }
        TakeDamage(damage);
        damaged = true ;
    }
    public void TakeDamage(int damage, PlayerForm form)
    {
        TakeDamage(damage, form, out bool damaged);
    }
    
    public void TakeDamage(int damage, PlayerForm form, out bool damaged)
    {
        if(form==PlayerForm.none)
        {
            TakeDamage(damage, out damaged);
        }
        else if(form!= currentForm)
        {
            TakeDamage(damage,out damaged);
        }
        else
        {
            damaged = false ;
        }
    }
   public int GetCurrentHealth()
    {
        return currentHealth;
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
