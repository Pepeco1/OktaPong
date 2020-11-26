using UnityEngine.Events;

public interface IShooter
{
    UnityAction OnKilledEnemy { get; set; }
    UnityAction OnShoot { get; set; }

    void SubscribeToProjectile(Projectile projectile);
}