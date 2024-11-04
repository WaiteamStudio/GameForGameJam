using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;
    [SerializeField]
    float invulnerabilityTime;
    private float lastTimeDamaged;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthComponent health = collision.gameObject.GetComponent<HealthComponent>();
        if (health != null)
        {
            Debug.Log($"Отправлен урон {damage} ");
            if (lastTimeDamaged + invulnerabilityTime > Time.time)
            {
                Debug.Log($" урон {damage} {gameObject.name }-м не нанесен, тик не прошел");
                return;
            }
            else
            {
                DealDamage(health, damage , collision,out bool damaged);
                if (damaged)
                {
                    lastTimeDamaged = Time.time;
                    Debug.Log($" урон {damage} {gameObject.name}-м нанесен");
                }
                else Debug.Log($" урон {damage} {gameObject.name}-м не нанесен, цель неуязвима");
            }

        }
    }
    //protected virtual void DealDamage(HealthComponent healthComponent, int damage, out bool damaged)
    //{ 
    //    healthComponent.TakeDamage(damage,out bool takenDamaged);
    //    damaged = takenDamaged;
    //}
    //protected virtual void DealDamage(HealthComponent healthComponent, int damage)
    //{ 
    //    healthComponent.TakeDamage(damage);
    //}
    protected virtual void DealDamage(HealthComponent healthComponent, int damage, Collision2D collision, out bool damaged)
    { 
        healthComponent.TakeDamage(new DamageParameters() {damage = damage,enemyCollision = collision}, out damaged);
    }
    //protected virtual void DealDamage(HealthComponent healthComponent, DamageParameters damageParameters, out bool damaged)
    //{ 
    //    healthComponent.TakeDamage(damageParameters,out damaged);
    //}
}
