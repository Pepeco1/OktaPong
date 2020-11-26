using UnityEngine.Events;

public interface IShooter
{
    UnityAction OnHit { get; set; }
    UnityAction OnKilledEnemy { get; set; }
    UnityAction OnShoot { get; set; }

    void SubscribeToProjectile(Projectile projectile);
}