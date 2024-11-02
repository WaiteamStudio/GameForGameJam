using UnityEngine;
public class ElementalDamageDealer : DamageDealer
{
    [SerializeField]
    private PlayerForm form;
    protected override void DealDamage(HealthComponent healthComponent, int damage)
    {
        healthComponent.TakeDamage(damage, form);
    }
}
