using UnityEngine;
public class ElementalDamageDealer : DamageDealer
{
    [SerializeField]
    private PlayerForm form;
    protected override void DealDamage(HealthComponent healthComponent, int damage,Collision2D collision, out bool damaged)
    {

        healthComponent.TakeDamage( new DamageParameters() { damage = damage, enemyCollision = collision,enemyForm = form},out damaged);
    }
    public void SetForm(PlayerForm playerForm)
    {
        form = playerForm;
    }
}
