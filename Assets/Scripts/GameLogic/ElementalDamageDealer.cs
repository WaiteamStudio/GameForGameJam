using UnityEngine;
public class ElementalDamageDealer : DamageDealer
{
    [SerializeField]
    private PlayerForm form;
    protected override void DealDamage(HealthComponent healthComponent, int damage)
    {
        healthComponent.TakeDamage(damage, form);
    }
    protected override void DealDamage(HealthComponent healthComponent, int damage, out bool damaged)
    {
        healthComponent.TakeDamage(damage, form,out damaged);
        damaged = false;
    }
    public void SetForm(PlayerForm playerForm)
    {
        form = playerForm;
    }
}
