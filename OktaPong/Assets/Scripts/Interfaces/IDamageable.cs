
public interface IDamageable
{
    Health Health { get; set; }

    void TakeDamage(int amount);
    void Heal(int amount);
}
