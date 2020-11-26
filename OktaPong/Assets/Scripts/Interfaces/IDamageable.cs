
using UnityEngine.Events;

public interface IDamageable : IHaveHealth
{
    bool TakeDamage(int amount);
    void Heal(int amount);


}
