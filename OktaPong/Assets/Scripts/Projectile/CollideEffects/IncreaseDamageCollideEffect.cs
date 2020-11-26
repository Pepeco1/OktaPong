using UnityEngine;

public class IncreaseDamageCollideEffect : ProjectileCollisionEffect
{

    [Tooltip("Amount to increase the damageMultiplier every bounce")]
    [SerializeField] private float damageMultiplierIncreasePerHit = 0.1f;

    protected override void Projectile_OnCollide()
    {
        myProjectile.DamageMultiplier += damageMultiplierIncreasePerHit;
    }
}
