using UnityEngine;

public partial class DamageDealer
{
    public class ElementalDamageDealer : DamageDealer
{ 
    [SerializeField]
    PlayerForm  playerForm;
    protected override void DealDamage(HealthComponent healthComponent, int damage)
    {
        healthComponent.TakeDamage((int)damage, playerForm);
    }
}
