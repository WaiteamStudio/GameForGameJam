public class PlayerTakeDamageEvent
{
    public PlayerTakeDamageEvent(int damage)
    {
        this.damage = damage;
    }
    public PlayerTakeDamageEvent(int damage, int currenthealth)
    {
        this.damage = damage;
        this.  currenthealth = currenthealth;
    }
    public int damage;
    public int currenthealth;
}
