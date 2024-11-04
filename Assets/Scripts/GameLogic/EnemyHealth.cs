using UnityEngine;

public class EnemyHealth : HealthComponent
{
    protected override void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);
        SoundManager.PlaySound(SoundManager.Sound.EnemyGetDamaged);
    }

    protected override void Die()
    {
        Debug.Log("Враг погиб.");
        // Логика для уничтожения или деактивации врага
        gameObject.SetActive(false);
    }
}
