using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : HealthComponent
{
    protected override void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);
        ServiceLocator.Current.Get<EventBus>().Invoke(new PlayerTakeDamageEvent(damage, currentHealth));
    }


    protected override void Die()
    {
        Debug.Log("Игрок погиб.");
        ServiceLocator.Current.Get<EventBus>().Invoke(new PlayerDiedEvent());
        gameObject.SetActive(false);
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }
}
