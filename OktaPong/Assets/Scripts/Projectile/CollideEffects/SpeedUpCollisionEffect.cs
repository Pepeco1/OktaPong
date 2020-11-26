using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class SpeedUpCollisionEffect : ProjectileCollisionEffect
{

    [Tooltip("Extra speed aplyed when the projectile collides")]
    [SerializeField] private float speedBoost = 3f;

    protected override void Projectile_OnCollide()
    {
        myProjectile.MaxSpeed += speedBoost;
    }

}
